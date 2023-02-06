using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Handler : MonoBehaviour
{
    #region Events
    public static event Action<int, int> Scored;
    public static event Action StartTimer;
    #endregion

    #region var
    [SerializeField] private Scores_Object dataSO;
    int totalScore = 0;
    public static bool canWin = true;
    List<int> _pool;
    List<int> _probability, _defaults;
    List<int> _slots;
    List<int> _scores;
    int k = 0, repeatAllowed = 3, internalCounter;
    [SerializeField] private int sProb = 10000;
    [SerializeField] private List<TextMeshProUGUI> slotText;
    [SerializeField] private List<Button> slotBtn;
    [SerializeField] private List<Sprite> slotImage;
    [SerializeField] private List<GameObject> slotAnim;
    #endregion
    private void OnEnable()
    {
        Bottom_Bar_Handler.BtnStart += Slots;
    }
    private void OnDisable()
    {
        Bottom_Bar_Handler.BtnStart -= Slots;
    }
    private void Awake()
    {
        _scores = dataSO._Score;
        _defaults = new List<int>() { 1, 5, 10, 0, 50, 0, 100, 250, 500, 1000, 2000 };
        _slots = new List<int>() { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    }
    private void Start()
    {
        //Slots();
    }

    #region Fill Slot
    public void Slots() // fill new slots
    {
        SlotCleaner();
        for (int i = 0; i < _slots.Count; i++)
        {
            FillTheSlot(i,k);
            k++;
        }
        StartCoroutine(TimeToShow());
    }
    private void ShowSlots(int i) //image and animation seq
    {
            SetImages(i);
            slotAnim[i].SetActive(false);
            slotBtn[i].gameObject.SetActive(true);
        
    }
    private void SlotCleaner() // refresh the variables
    {
        Scored?.Invoke(0, 0);
        totalScore = 0;
        //canWin = true;
        repeatAllowed = 3;
        internalCounter = 0;
        _slots = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1, -1};
        _pool = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        _defaults = new List<int>() { 1, 5, 10, 0, 50, 0, 100, 250, 500, 1000, 2000 };
        _probability = new List<int>() { sProb, sProb, sProb, sProb, sProb, sProb, sProb, sProb, sProb, sProb, sProb };
        k = 0;

        for (int i = 0; i < 9; i++)
        {
            slotBtn[i].gameObject.SetActive(false);
            slotAnim[i].SetActive(true);
        }
    }
    private void FillTheSlot(int index, int element) // calculate how to fill slots
    {
        int rnd = RandomGenerator(RangeSum(_probability));
        int newElement = CheckRange(rnd);
        UpdateProbability(newElement);
        _slots[index] = _pool[newElement];
        slotText[element].text = _slots[index].ToString();
        CheckSlots(index);

    }
    private int RangeSum(List<int> items) //sum of all ranges
    {
        int sum = 0;
        foreach (int item in items)
        {
            sum += item;
        }
        return sum;
    }
    private int RandomGenerator(int max) // generate random in range
    {
        return UnityEngine.Random.Range(1, max);
    }
    private int CheckRange(int number) // find which 11 element is in range
    {
        int tempMax = _probability[0], tempMin = 0;
        for (int i = 0; i < 11; i++)
        {
            if (number > tempMin && number <= tempMax)
            {
                return i;
            }
            tempMin = tempMax;
            tempMax += _probability[i + 1];
        }
        return 0;
    }
    private void UpdateProbability(int number) // update proabilities after finding elements;
    {
        _probability[number] = _defaults[number];//(int)((float)(_probability[number] / sProb) * 
        _defaults[number] = 0;
    }
    private void SetImages(int btnNum) //set image of slots
    {
        slotBtn[btnNum].image.sprite = slotImage[_slots[btnNum]];
    }
    private void CheckSlots(int _index) //check for prev slots
    {
        int counter = 0;
        int redundant = -1;

        foreach (int item in _slots)
        {
            if (_slots[_index] == item)
            {
                counter++;
                redundant = item;
                internalCounter++;
            }
        }
        if (counter >= repeatAllowed)
        {
            _pool.Remove(redundant);

            if (internalCounter > 1)
            {
                print("internal >1");
                repeatAllowed = 2;
            }
            FillTheSlot(_index, k);
        }

    }
    #endregion

    #region Buttons & Score 
    public void CheckScore_0()
    {
        CountScore(1, 2);
        CountScore(3, 6);
        CountScore(4, 8);
        if (totalScore > 0) TransferScore(0);
    }
    public void CheckScore_1()
    {
        CountScore(0, 2);
        CountScore(4, 7);
        if (totalScore > 0) TransferScore(1);
    }
    public void CheckScore_2()
    {
        CountScore(0, 1);
        CountScore(4, 6);
        CountScore(5, 8);
        if (totalScore > 0) TransferScore(2);
    }
    public void CheckScore_3()
    {
        CountScore(0, 6);
        CountScore(4, 5);
        if (totalScore > 0) TransferScore(3);
    }
    public void CheckScore_4()
    {
        CountScore(1, 7);
        CountScore(3, 5);
        CountScore(0, 8);
        CountScore(2, 6);
        if (totalScore > 0) TransferScore(4);
    }
    public void CheckScore_5()
    {
        CountScore(2, 8);
        CountScore(3, 4);
        if (totalScore > 0) TransferScore(5);
    }
    public void CheckScore_6()
    {
        CountScore(0, 3);
        CountScore(4, 2);
        CountScore(7, 8);
        if (totalScore > 0) TransferScore(6);
    }
    public void CheckScore_7()
    {
        CountScore(1, 4);
        CountScore(6, 8);
        if (totalScore > 0) TransferScore(7);
    }
    public void CheckScore_8()
    {
        CountScore(0, 4);
        CountScore(2, 5);
        CountScore(6, 7);
        if (totalScore > 0) TransferScore(8);
    }

    private void CountScore(int i, int j)
    {
        if (canWin && _slots[i] == _slots[j])
        {
            totalScore += _scores[_slots[i]];
        }
    }
    private void TransferScore(int clickedBtn)
    {
        canWin = false;
        int bonusVal = _slots[clickedBtn];
        Scored?.Invoke(totalScore, bonusVal);
        totalScore = 0;
    }
    #endregion

    #region IEnum
    IEnumerator TimeToShow()
    {
        yield return new WaitForSeconds(1);
        ShowSlots(0);
        yield return new WaitForSeconds(.1f);
        ShowSlots(1);
        yield return new WaitForSeconds(.1f);
        ShowSlots(2);
        yield return new WaitForSeconds(.1f);
        ShowSlots(3);
        yield return new WaitForSeconds(.1f);
        ShowSlots(4);
        yield return new WaitForSeconds(.1f);
        ShowSlots(5);
        yield return new WaitForSeconds(.1f);
        ShowSlots(6);
        yield return new WaitForSeconds(.1f);
        ShowSlots(7);
        yield return new WaitForSeconds(.1f);
        ShowSlots(8);
        canWin = true;
        StartTimer?.Invoke();

    }
    #endregion
}
