using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : SingletonBehaviour<CarManager>
{
    [SerializeField] private float _driverWeight = 40f;
    [SerializeField] private float _reverseSpeedLimit = 20f;

    protected override CarManager This
    {
        get
        {
            return this;
        }
    }

    public float DriverWeight
    {
        get
        {
            return _driverWeight;
        }
    }

    public float ReverseSpeedLimit
    {
        get
        {
            return _reverseSpeedLimit;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
