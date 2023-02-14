using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelValue
{
    private Transform _tire;
    private WheelCollider _collider;

    public Transform Tire
    {
        get
        {
            return _tire;
        }
    }

    public WheelCollider Collider
    {
        get
        {
            return _collider;
        }
    }

    public CarWheelValue(WheelCollider collider, Transform tire)
    {
        _tire = tire;
        _collider = collider;
    }
}
