using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSlip : MonoBehaviour
{
    [SerializeField] private Image _extremumSlipImage;
    [SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitSlip(float extremumSlip, float asymptoteSlip)
    {
        float anchor;

        if (asymptoteSlip == 0f)
            anchor = 0f;
        else
        {
            anchor = extremumSlip / asymptoteSlip;

            if (_slider.direction == Slider.Direction.RightToLeft)
                anchor = 1f - anchor;
        }

        RectTransform rectTransform = _extremumSlipImage.rectTransform;

        Vector2 anchorMin = rectTransform.anchorMin;
        Vector2 anchorMax = rectTransform.anchorMax;

        anchorMin.x = anchorMax.x = anchor;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        _slider.maxValue = asymptoteSlip;
    }

    public void SetSlip(float slip)
    {
        _slider.value = Mathf.Abs(slip);
    }
}
