using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinPopUp_Handler : MonoBehaviour
{
    public static event Action<int> WinValue;
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
        int size = WinSize(score);
        int bonusMiltiply = WinBonus(bonus);
        int total = score * bonusMiltiply;
        print("Total: " + total);
        WinValue?.Invoke(total);

    }

    private int WinSize(int _score)
    {
        if (_score < 200) return 0;
        else if (_score < 5000) return 1;
        else if (_score < 20000) return 2;
        else return 3;
    }
    private int WinBonus(int _bonus)
    {
        if (_bonus == 3) return 3;
        else if (_bonus == 5) return 2;
        else return 1;
    }
}
