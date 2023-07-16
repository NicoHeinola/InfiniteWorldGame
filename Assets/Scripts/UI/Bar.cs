using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] protected Slider slider;

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }

    public void SetMinValue(int value)
    {
        slider.minValue = value;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
}
