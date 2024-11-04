using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways()]
public class ProgressBar : MonoBehaviour
{
    public Image FillRect = null;
    public Slider.Direction Direction = Slider.Direction.LeftToRight;
    public float minValue = 0;
    public float maxValue = 100.0f;
    public float value = 50.0f;

    // Update is called once per frame
    void Update()
    {
        RectTransform rt = FillRect.GetComponent<RectTransform>();
        switch (Direction)
        {
            case Slider.Direction.LeftToRight:
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = new Vector2(value / maxValue, 1.0f);
                break;
            case Slider.Direction.RightToLeft:
                rt.anchorMin = new Vector2(1.0f - value / maxValue, 0.0f);
                rt.anchorMax = Vector2.one;
                break;
            case Slider.Direction.BottomToTop:
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = new Vector2(1.0f, value / maxValue);
                break;
            case Slider.Direction.TopToBottom:
                rt.anchorMin = new Vector2(0.0f, 1.0f - value / maxValue);
                rt.anchorMax = Vector2.one;
                break;
        }
    }
}
