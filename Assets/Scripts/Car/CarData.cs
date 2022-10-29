using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "GOIS/CarData")]
public class CarData : ScriptableObject
{
    [Range(1f, 6000f)][SerializeField] private int _finalGear = 3000;
    [Range(1f, 6000f)][SerializeField] private int[] _gearRatio = DefaultGearRatio;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private float _tunedBrakeTorque;
    [SerializeField] private float _tunedBrakeWeight;
    [SerializeField] private float _tunedSusupensionSpring = 35000f;
    [SerializeField] private float _tunedSuspensionDamper = 4500f;
    [SerializeField] private string _carName;
    [SerializeField] private CarAspirationType _aspirationType;
    [SerializeField] private CarTuneData[] _tuneDatas = new CarTuneData[21];
    [SerializeField]
    private CarWheelFrictionData _tunedForwardFriction
        = CarWheelFrictionData.DefaultForwardFriction;
    [SerializeField]
    private CarWheelFrictionData _tunedSidewaysFriction
        = CarWheelFrictionData.DefaultSidewaysFriction;

    public int FinalGear
    {
        get
        {
            return _finalGear;
        }
    }

    public int[] GearRatio
    {
        get
        {
            return _gearRatio;
        }
    }

    public float BrakeTorque
    {
        get
        {
            return _brakeTorque;
        }
    }

    public float TunedBrakeTorque
    {
        get
        {
            return _tunedBrakeTorque;
        }
    }

    public float TunedBrakeWeight
    {
        get
        {
            return _tunedBrakeWeight;
        }
    }

    public float TunedSusupensionSpring
    {
        get
        {
            return _tunedSusupensionSpring;
        }
    }

    public float TunedSuspensionDamper
    {
        get
        {
            return _tunedSuspensionDamper;
        }
    }

    public string CarName
    {
        get
        {
            return _carName;
        }
    }

    public CarAspirationType AspirationType
    {
        get
        {
            return _aspirationType;
        }
    }

    public WheelFrictionCurve TunedForwardFriction
    {
        get
        {
            return _tunedForwardFriction.ToWheelFrictionCurve();
        }
    }

    public WheelFrictionCurve TunedSidewaysFriction
    {
        get
        {
            return _tunedSidewaysFriction.ToWheelFrictionCurve();
        }
    }

    static int[] DefaultGearRatio
    {
        get
        {
            int[] gearRetio = new int[6];

            for (int i = 0; i < gearRetio.Length; i++)
                gearRetio[i] = 3000;

            return gearRetio;
        }
    }

    public CarSubTune[] GetSubTunes(int tuneLevel)
    {
        return _tuneDatas.Take(tuneLevel + 1).Select(d => d.SubTune).Where(t => t != CarSubTune.None).ToArray();
    }

    public AnimationCurve GetEngineTorqueCurve(int tuneLevel)
    {
        return _tuneDatas[tuneLevel].EngineTorqueCurve;
    }
}
