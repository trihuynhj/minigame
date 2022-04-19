using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinMaxPoints(float maxPoint)
    {
        slider.minValue = 0f;
        slider.maxValue = maxPoint;
    }

    public void SetPoint(float points)
    {
        slider.value = points;
    }
}
