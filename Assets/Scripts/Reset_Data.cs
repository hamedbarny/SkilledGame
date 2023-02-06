using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Data : MonoBehaviour
{
    [SerializeField] private Scores_Object scoreSO;
    [Range(40, 400)] [SerializeField] private int startLevel;
    [Range(2, 20)] [SerializeField] private int timeFrame;
    [SerializeField] private int startPoint;
    private void Awake()
    {
        scoreSO._9Scores[8] = startLevel / 4;
        scoreSO._9Scores[7] = startLevel / 2;
        scoreSO._9Scores[6] = startLevel;
        scoreSO._9Scores[5] = startLevel * 2;
        scoreSO._9Scores[4] = startLevel * 5;
        scoreSO._9Scores[3] = startLevel * 10;
        scoreSO._9Scores[2] = startLevel * 50;
        scoreSO._9Scores[1] = startLevel * 100;
        scoreSO._9Scores[0] = startLevel * 500;

        scoreSO.timeFrame = timeFrame;

        scoreSO.startPoints = startPoint;
    }
}
