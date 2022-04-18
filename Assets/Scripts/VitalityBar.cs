using UnityEngine;
using UnityEngine.UI;

public class VitalityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinMaxPoints(int minPoint, int maxPoint)
    {
        slider.minValue = minPoint;
        slider.maxValue = maxPoint;
    }

    public void SetPoint(int point)
    {
        slider.value = point;
    }
}