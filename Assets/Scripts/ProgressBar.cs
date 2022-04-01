using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPoint(int maxPoint)
    {
        slider.maxValue = maxPoint;
    }

    public void SetPoint(int point)
    {
        slider.value = point;
    }
}
