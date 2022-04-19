using UnityEngine;
using UnityEngine.UI;

public class VitalityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinMaxPoints(float maxPoints)
    {
        slider.minValue = 0f;
        slider.maxValue = maxPoints;
    }

    public void SetPoint(float points)
    {
        slider.value = points;
    }
}