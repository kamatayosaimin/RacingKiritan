using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarEngineTypeName : KeyValuePairC<CarEngineType, string>
{

    public CarEngineTypeName()
        : base()
    {
    }

    public CarEngineTypeName(CarEngineType key, string value)
        : base(key, value)
    {
    }
}
