using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelSlip
{
    private CarSlip _forwardSlip;
    private CarSlip _sidewaysSlip;
    private CarWheelStatus _status;

    public CarWheelSlip(CarWheelStatus status, CarSlip forwardSlip, CarSlip sidewaysSlip)
    {
        _forwardSlip = forwardSlip;
        _sidewaysSlip = sidewaysSlip;
        _status = status;
    }

    public void Init()
    {
        WheelCollider wheelCollider = _status.Collider;

        WheelFrictionCurve forwardFriction = wheelCollider.forwardFriction;
        WheelFrictionCurve sidewaysFriction = wheelCollider.sidewaysFriction;

        _forwardSlip.InitSlip(forwardFriction.extremumSlip, forwardFriction.asymptoteSlip);

        _sidewaysSlip.InitSlip(sidewaysFriction.extremumSlip, sidewaysFriction.asymptoteSlip);
    }

    public void SetSlip()
    {
        WheelHit hit = _status.Hit;

        _forwardSlip.SetSlip(hit.forwardSlip);

        _sidewaysSlip.SetSlip(hit.sidewaysSlip);
    }
}
