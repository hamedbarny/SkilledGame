using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class WinPopUp_Handler : MonoBehaviour
{
    public static event Action<int> WinValue;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private TextMeshProUGUI txtScore,txtTitle;
    [SerializeField] private Image burst;


    private void OnEnable()
    {
        Game_Handler.Scored += WinHandler;
    }
    private void OnDisable()
    {
        Game_Handler.Scored -= WinHandler;
    }

    private void WinHandler(int score,int bonus)
    {
        int bonusMiltiply = WinBonus(bonus);
        int total = score * bonusMiltiply;
        int size = WinSize(total);
        WinValue?.Invoke(total);
        if (total > 0)
            ShowScore(total, size);
    }

    private void ShowScore(int _score,int _size)
    {
        Color clr = new Color();
        panelWin.SetActive(true);
        switch (_size)
        {
            case 0:
                txtTitle.text = "WIN";
                clr = new Color(.35f, .15f, .5f, .8f);
                burst.color = clr;
                break;
            case 1:
                txtTitle.text = "GOOD WIN";
                clr = new Color(.1f, .5f, .9f, .8f);
                burst.color = clr;
                break;
            case 2:
                txtTitle.text = "BIG WIN";
                clr = new Color(.9f, .8f, .4f, .8f);
                burst.color = clr;
                break;
        }
        StartCoroutine(CountUpToTarget(_score));
    }

    private int WinSize(int _score)
    {
        if (_score < 500) return 0;
        else if (_score < 5000) return 1;
        else return 2;
    }
    private int WinBonus(int _bonus)
    {
        if (_bonus == 3) return 3;
        else if (_bonus == 5) return 2;
        else return 1;
    }

    IEnumerator CountUpToTarget(int _score)
    {
        float _total = 0;
        while (_total < _score)
        {
            _total += Time.deltaTime * _score / 2;
            _total = Mathf.Clamp(_total, 0f, _score);
            txtScore.text = (int)_total+"";
            yield return null;
        }
        yield return new WaitForSeconds(1);
        panelWin.SetActive(false);
    }
}
