using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Difficulty_Handler : MonoBehaviour
{
    public static event Action<int> DifficultyChanged;

    [SerializeField] private Slider _slider;
    public void SliderChange()
    {
        DifficultyChanged?.Invoke((int)_slider.value);
    }
}
