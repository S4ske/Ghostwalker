using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParameter : MonoBehaviour
{
    private float currentValue;
    private float maxValue;

    [SerializeField] private Text text;
    [SerializeField] private Slider slider;
    
    void Update()
    {
        text.text = $"{Math.Round(currentValue, 1)}/{maxValue}";
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void SetStat(float current, float max)
    {
        currentValue = current;
        maxValue = max;
    }
}
