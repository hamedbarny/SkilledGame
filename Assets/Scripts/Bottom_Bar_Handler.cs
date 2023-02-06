using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Bottom_Bar_Handler : MonoBehaviour
{
    public static event Action BtnStart, BtnMax, BtnUp, BtnDown;
    public static event Action<int> LevelUpdated;
    public static event Action<int> StartCost;
    public static event Action NeedMorePoint;

    [SerializeField] private Scores_Object scoreSO;
    [SerializeField] private Button Start_Button;
    [SerializeField] private GameObject topTextBar;
    private int currentLevel = 40;
    private int levelCounter = 1;

    public void Btn_Start()
    {
        if (topTextBar.activeInHierarchy) topTextBar.SetActive(false);
        if (scoreSO.points >= currentLevel)
        {
            ButtonStartCoolDown();
            AffectUpdateScore();
            BtnStart?.Invoke();
            StartCost?.Invoke(currentLevel * -1);
        }
        else NeedMorePoint?.Invoke();
    }
    #region btns
    public void Btn_Max()
    {
        currentLevel = 400;
        levelCounter = 10;
        UpdateScores(400);
        BtnMax?.Invoke();
    }
    public void Btn_LevelUp()
    {
        UpdateLevel(1);
        UpdateScores(currentLevel);
        BtnUp?.Invoke();
    }
    public void Btn_LevelDown()
    {
        UpdateLevel(-1);
        UpdateScores(currentLevel);
        BtnDown?.Invoke();
    }
    #endregion
    private void UpdateLevel(int upOrDown)
    {
        levelCounter += upOrDown;
        if (levelCounter < 1) levelCounter = 1;
        if (levelCounter > 10) levelCounter = 10;
        currentLevel = levelCounter * 40;
    }
    private void UpdateScores(int level)
    {
        scoreSO._9Scores[8] = level / 4;
        scoreSO._9Scores[7] = level / 2;
        scoreSO._9Scores[6] = level;
        scoreSO._9Scores[5] = level * 2;
        scoreSO._9Scores[4] = level * 5;
        scoreSO._9Scores[3] = level * 10;
        scoreSO._9Scores[2] = level * 50;
        scoreSO._9Scores[1] = level * 100;
        scoreSO._9Scores[0] = level * 500;

        LevelUpdated?.Invoke(level);
    }
    private void AffectUpdateScore()
    {
        scoreSO._Score[10] = scoreSO._9Scores[8];
        scoreSO._Score[9] = scoreSO._9Scores[7];
        scoreSO._Score[8] = scoreSO._9Scores[6];
        scoreSO._Score[7] = scoreSO._9Scores[5];
        scoreSO._Score[6] = scoreSO._9Scores[4];
        scoreSO._Score[4] = scoreSO._9Scores[3];
        scoreSO._Score[2] = scoreSO._9Scores[2];
        scoreSO._Score[1] = scoreSO._9Scores[1];
        scoreSO._Score[0] = scoreSO._9Scores[0];
    }
    private void ButtonStartCoolDown()
    {
        StartCoroutine(BtnStartCD());
    }


    IEnumerator BtnStartCD()
    {
        Start_Button.interactable = false;
        yield return new WaitForSeconds(2.5f);
        Start_Button.interactable = true;
    }
}
