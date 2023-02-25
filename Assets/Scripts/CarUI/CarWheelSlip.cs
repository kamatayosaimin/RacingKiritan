using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelSlip
{
    private WheelCollider _wheelCollider;
    private CarSlip _forwardSlip;
    private CarSlip _sidewaysSlip;

    public CarWheelSlip(WheelCollider wheelCollider, CarSlip forwardSlip, CarSlip sidewaysSlip)
    {
        _wheelCollider = wheelCollider;
        _forwardSlip = forwardSlip;
        _sidewaysSlip = sidewaysSlip;
    }

    public void Init()
    {
        WheelFrictionCurve forwardFriction = _wheelCollider.forwardFriction;
        WheelFrictionCurve sidewaysFriction = _wheelCollider.sidewaysFriction;

        _forwardSlip.InitSlip(forwardFriction.extremumSlip, forwardFriction.asymptoteSlip);

        _sidewaysSlip.InitSlip(sidewaysFriction.extremumSlip, sidewaysFriction.asymptoteSlip);
    }

    public void SetSlip()
    {
        WheelHit hit;

        _wheelCollider.GetGroundHit(out hit);

        _forwardSlip.SetSlip(hit.forwardSlip);

        _sidewaysSlip.SetSlip(hit.sidewaysSlip);
    }
}
