using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Handler : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> scoreText;
    [SerializeField] private Scores_Object dataSO;

    private void OnEnable()
    {
        Bottom_Bar_Handler.BtnMax += UpdateScoreText;
        Bottom_Bar_Handler.BtnUp += UpdateScoreText;
        Bottom_Bar_Handler.BtnDown += UpdateScoreText;
    }
    private void OnDisable()
    {
        Bottom_Bar_Handler.BtnMax -= UpdateScoreText;
        Bottom_Bar_Handler.BtnUp -= UpdateScoreText;
        Bottom_Bar_Handler.BtnDown -= UpdateScoreText;
    }
    private void UpdateScoreText()
    {
        for (int i = 0; i < scoreText.Count; i++)
        {
            scoreText[i].text = dataSO._9Scores[i].ToString();
        }
    }
}
