using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUIControllerDRun : CarUIControllerBase
{
    private List<CarWheelSlip> _wheelSlipList;
    [SerializeField] private TMPro.TextMeshProUGUI _steeringText;
    [SerializeField] private CarSlider _steeringSlider;
    [SerializeField] private CarSlip _forwardSlipFL;
    [SerializeField] private CarSlip _forwardSlipFR;
    [SerializeField] private CarSlip _forwardSlipRL;
    [SerializeField] private CarSlip _forwardSlipRR;
    [SerializeField] private CarSlip _sidewaysSlipFL;
    [SerializeField] private CarSlip _sidewaysSlipFR;
    [SerializeField] private CarSlip _sidewaysSlipRL;
    [SerializeField] private CarSlip _sidewaysSlipRR;

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
        try
        {
            foreach (var ws in _wheelSlipList)
                ws.SetSlip();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void InitChild()
    {
        try
        {
            CarWheelDictionary<WheelCollider> wheelColliderDictinary = PlayerCarController.GetWheelColliderDictionary();
            CarWheelDictionary<CarSlip> forwardSlipDictionary = new CarWheelDictionary<CarSlip>();
            CarWheelDictionary<CarSlip> sidewaysSlipDictionary = new CarWheelDictionary<CarSlip>();

            forwardSlipDictionary.Add(CarWheelPosition.FrontLeft, _forwardSlipFL);
            forwardSlipDictionary.Add(CarWheelPosition.FrontRight, _forwardSlipFR);
            forwardSlipDictionary.Add(CarWheelPosition.RearLeft, _forwardSlipRL);
            forwardSlipDictionary.Add(CarWheelPosition.RearRight, _forwardSlipRR);

            sidewaysSlipDictionary.Add(CarWheelPosition.FrontLeft, _sidewaysSlipFL);
            sidewaysSlipDictionary.Add(CarWheelPosition.FrontRight, _sidewaysSlipFR);
            sidewaysSlipDictionary.Add(CarWheelPosition.RearLeft, _sidewaysSlipRL);
            sidewaysSlipDictionary.Add(CarWheelPosition.RearRight, _sidewaysSlipRR);

            _wheelSlipList = new List<CarWheelSlip>();

            foreach (var k in EnumUtil.GetValues<CarWheelPosition>())
            {
                WheelCollider wheelCollider = wheelColliderDictinary[k];
                CarSlip forwardSlip = forwardSlipDictionary[k];
                CarSlip sidewaysSlip = sidewaysSlipDictionary[k];
                CarWheelSlip wheelSlip = new CarWheelSlip(wheelCollider, forwardSlip, sidewaysSlip);

                wheelSlip.Init();

                _wheelSlipList.Add(wheelSlip);
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }
}
