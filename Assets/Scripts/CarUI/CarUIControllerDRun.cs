using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUIControllerDRun : CarUIControllerBase
{
    [SerializeField] private TMPro.TextMeshProUGUI _steeringText;
    [SerializeField] private CarSlider _steeringSlider;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void SetSteeringText(float value)
    {
        try
        {
            _steeringText.text = GetInputString(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetSteeringSliderValue(float value)
    {
        try
        {
            _steeringSlider.value = value;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void StartChild()
    {
    }

    protected override void UpdateChild()
    {
    }
}
