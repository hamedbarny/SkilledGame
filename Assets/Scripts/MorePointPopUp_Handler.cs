using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePointPopUp_Handler : MonoBehaviour
{
    [SerializeField] private GameObject _lowGoldWindow, _gameOverWindow;
    private void OnEnable()
    {
        Bottom_Bar_Handler.NeedMorePoint += LowPointWindow;
        ScoreBoard_Handler.GameIsOver += GameOverWindow;
    }
    private void OnDisable()
    {
        Bottom_Bar_Handler.NeedMorePoint -= LowPointWindow;
        ScoreBoard_Handler.GameIsOver -= GameOverWindow;
    }
    private void GameOverWindow()
    {
        StartCoroutine(ToggleWindow(_gameOverWindow, 5f));
    }
    private void LowPointWindow()
    {
        StartCoroutine(ToggleWindow(_lowGoldWindow, 2f));
    }
    IEnumerator ToggleWindow(GameObject go,float _time)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(_time);
        go.SetActive(false);
    }
}
