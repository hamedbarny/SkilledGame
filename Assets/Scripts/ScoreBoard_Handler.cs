using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard_Handler : MonoBehaviour
{
    public static event Action GameIsOver;

    [SerializeField] private TextMeshProUGUI txtLevel, txtPoint, txtWin, txtTime;
    [SerializeField] private Scores_Object scoreSO;
    [SerializeField] private AudioClip tikAudio, timeUpAudio;

    bool isTimerRunning = false;
    float totalTime = 4;
    int timeDif = 0;
    Transform ears;

    private void Start()
    {
        ears = Camera.main.transform;
        totalTime = scoreSO.timeFrame;
        txtPoint.text = scoreSO.startPoints.ToString();
        scoreSO.points = scoreSO.startPoints;
    }
    private void OnEnable()
    {
        Bottom_Bar_Handler.LevelUpdated += UpdateLevel;
        WinPopUp_Handler.WinValue += UpdateWin;
        Game_Handler.StartTimer += UpdateTimer;
        Bottom_Bar_Handler.BtnStart += StopTimer;
        Bottom_Bar_Handler.StartCost += UpdatePoint;
        Game_Handler.StopTimer += StopTimer;
    }
    private void OnDisable()
    {
        Bottom_Bar_Handler.LevelUpdated -= UpdateLevel;
        WinPopUp_Handler.WinValue -= UpdateWin;
        Game_Handler.StartTimer -= UpdateTimer;
        Bottom_Bar_Handler.BtnStart -= StopTimer;
        Bottom_Bar_Handler.StartCost -= UpdatePoint;
        Game_Handler.StopTimer -= StopTimer;
    }
    private void Update()
    {
        if (isTimerRunning)
        {
            int timeToInt = (int)totalTime;
            txtTime.text = timeToInt.ToString();
            totalTime -= Time.deltaTime;
            if(timeToInt != timeDif)
            {
                AudioSource.PlayClipAtPoint(tikAudio, ears.position);
                timeDif = timeToInt;
            }
            if (totalTime <= 0)
            {
                if (scoreSO.points < 40)
                {
                    GameIsOver?.Invoke();
                }
                AudioSource.PlayClipAtPoint(timeUpAudio, ears.position);
                txtTime.text = "TIME IS UP";
                Game_Handler.canWin = false;
                isTimerRunning = false;
            }
        }
        
    }
    private void UpdateLevel(int level)
    {
        txtLevel.text = level.ToString();
    }
    private void UpdateWin(int total)
    {
        txtWin.text = total.ToString();
        UpdatePoint(total);
    }
    private void UpdateTimer()
    {
        isTimerRunning = true;
        totalTime = scoreSO.timeFrame;
    }
    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdatePoint(int _point)
    {
        scoreSO.points += _point;
        txtPoint.text = scoreSO.points.ToString();

    }

}
