using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarManager : SingletonBehaviour<CarManager>
{
    [SerializeField] private float _driverWeight = 40f;
    [SerializeField] private float _reverseSpeedLimit = 20f;
    [SerializeField] private float _accelOnBoder;
    [SerializeField] private float _accelOffSpan;
    [SerializeField] private CarEngineTypeName[] _engineTypeNames = DefaultEngineTypeNames;

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

    public float AccelOnBoder
    {
        get
        {
            return _accelOnBoder;
        }
    }

    public float AccelOffSpan
    {
        get
        {
            return _accelOffSpan;
        }
    }

    static CarEngineTypeName[] DefaultEngineTypeNames
    {
        get
        {
            System.Func<CarEngineType, CarEngineTypeName> selector = k =>
            {
                CarEngineTypeName name = new CarEngineTypeName();

                name.Key = k;

                return name;
            };

            return EnumUtil.GetValues<CarEngineType>().Select(selector).ToArray();
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
