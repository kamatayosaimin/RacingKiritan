using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarWheelFrictionData
{
    [SerializeField] private float _extremumSlip;
    [SerializeField] private float _extremumValue;
    [SerializeField] private float _asymptoteSlip;
    [SerializeField] private float _asymptoteValue;
    [SerializeField] private float _stiffness;

    public static CarWheelFrictionData DefaultForwardFriction
    {
        get
        {
            CarWheelFrictionData data = new CarWheelFrictionData();

            data._extremumSlip = 0.4f;
            data._extremumValue = 1f;
            data._asymptoteSlip = 0.8f;
            data._asymptoteValue = 0.5f;
            data._stiffness = 1f;

            return data;
        }
    }

    public static CarWheelFrictionData DefaultSidewaysFriction
    {
        get
        {
            CarWheelFrictionData data = new CarWheelFrictionData();

            data._extremumSlip = 0.2f;
            data._extremumValue = 1f;
            data._asymptoteSlip = 0.5f;
            data._asymptoteValue = 0.75f;
            data._stiffness = 1f;

            return data;
        }
    }

    public CarWheelFrictionData()
    {
    }

    public WheelFrictionCurve ToWheelFrictionCurve()
    {
        WheelFrictionCurve curve = new WheelFrictionCurve();

        curve.extremumSlip = _extremumSlip;
        curve.extremumValue = _extremumValue;
        curve.asymptoteSlip = _asymptoteSlip;
        curve.asymptoteValue = _asymptoteValue;
        curve.stiffness = _stiffness;

        return curve;
    }
}
