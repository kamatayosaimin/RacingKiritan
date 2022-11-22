using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private int _shiftIndex = 0;
    private float _engineRpm;
    private float _brakeTorque;
    private float _steering;
    private float _finalGear;
    private float[] _gearRatio;
    private CarAspirationType _aspirationType;
    private CarDriveType _driveType;
    private AnimationCurve _engineTorqueCurve;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private Transform _tireFL;
    [SerializeField] private Transform _tireFR;
    [SerializeField] private Transform _tireRL;
    [SerializeField] private Transform _tireRR;
    [SerializeField] private WheelCollider _wheelColliderFL;
    [SerializeField] private WheelCollider _wheelColliderFR;
    [SerializeField] private WheelCollider _wheelColliderRL;
    [SerializeField] private WheelCollider _wheelColliderRR;
    private CarInputBase _input;
    private CarSoundController _soundController;

    /// <summary>
    /// ÉzÉCÅ[Éãíºåa(2) * íPà í≤êÆåWêî(60 / 1000)
    /// </summary>
    private const float WheelRateMultiple = 0.12f;

    void Awake()
    {
        try
        {
            _rigidbody = GetComponent<Rigidbody>();
            _soundController = GetComponent<CarSoundController>();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _rigidbody.centerOfMass = _centerOfMass.localPosition;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitData(CarData data, int tuneLevel)
    {
        try
        {
            _brakeTorque = data.BrakeTorque;
            _steering = data.Steering;
            _finalGear = data.FinalGear / 1000f;
            _gearRatio = data.GearRatio.Select(r => r / 1000f).ToArray();
            _driveType = GetDriveType(data.DriveType);
            _engineTorqueCurve = data.GetEngineTorqueCurve(tuneLevel);

            CarSubTune[] subTunes = data.GetSubTunes(tuneLevel);

            _aspirationType = GetAspirationType(data.AspirationType, subTunes);

            WheelCollider[] wheelColliders = GetAllWheelColliders();

            if (IsTuned(subTunes, CarSubTune.Tire))
                foreach (var wc in wheelColliders)
                {
                    wc.forwardFriction = data.TunedForwardFriction;
                    wc.sidewaysFriction = data.TunedSidewaysFriction;
                }

            if (IsTuned(subTunes, CarSubTune.Suspension))
                foreach (var wc in wheelColliders)
                {
                    JointSpring spring = wc.suspensionSpring;

                    spring.spring = data.TunedSusupensionSpring;
                    spring.damper = data.TunedSuspensionDamper;

                    wc.suspensionSpring = spring;
                }

            if (IsTuned(subTunes, CarSubTune.WeightDown))
                _rigidbody.mass -= data.WeightDownValue;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitInput(CarInputBase input)
    {
        _input = input;
    }

    public void InitSound(CarSoundClipData clipData, CarSoundPitchData pitchData)
    {
        try
        {
            if (!_soundController)
                return;

            _soundController.InitTireSounds(GetTireSoundDictionary());
            _soundController.InitClipData(clipData);
            _soundController.InitPitchData(pitchData);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public float GetTotalMass()
    {
        float mass = _rigidbody.mass;

        mass += GetAllWheelColliders().Sum(wc => wc.mass);

        return mass;
    }

    /// <summary>
    /// É^ÉCÉÑâ~é¸ * íPà í≤êÆåWêî
    /// </summary>
    /// <returns></returns>
    float GetWheelRate()
    {
        float radiusAverage = GetAllWheelColliders().Average(wc => wc.radius);

        return Mathf.PI * radiusAverage * WheelRateMultiple;
    }

    bool IsTuned(CarSubTune[] subTunes, CarSubTune target)
    {
        return subTunes.Any(t => t == target);
    }

    CarAspirationType GetAspirationType(CarAspirationType type, CarSubTune[] subTunes)
    {
        switch (type)
        {
            case CarAspirationType.NA:
                bool isTuned = subTunes.Any(t => t == CarSubTune.Turbo || t == CarSubTune.EngineChange);

                return isTuned ? CarAspirationType.Turbo : CarAspirationType.NA;
            case CarAspirationType.Turbo:
                return CarAspirationType.Turbo;
            default:
                throw new ArgumentException();
        }
    }

    CarDriveType GetDriveType(CarDriveTypeStatus status)
    {
        switch (status)
        {
            case CarDriveTypeStatus.FF:
                return CarDriveType.Front;
            case CarDriveTypeStatus.FR:
                return CarDriveType.Rear;
            case CarDriveTypeStatus.MR:
                return CarDriveType.Rear;
            case CarDriveTypeStatus.RR:
                return CarDriveType.Rear;
            case CarDriveTypeStatus.FourWD:
                return CarDriveType.FourWheelDrive;
            default:
                throw new ArgumentException();
        }
    }

    Dictionary<CarWheelPosition, AudioSource> GetTireSoundDictionary()
    {
        return GetWheelColliderDictionary().ToDictionary(p => p.Key, p => p.Value.GetComponent<AudioSource>());
    }

    Dictionary<CarWheelPosition, WheelCollider> GetWheelColliderDictionary()
    {
        Dictionary<CarWheelPosition, WheelCollider> dictionary = new Dictionary<CarWheelPosition, WheelCollider>();

        dictionary.Add(CarWheelPosition.FrontLeft, _wheelColliderFL);
        dictionary.Add(CarWheelPosition.FrontRight, _wheelColliderFR);
        dictionary.Add(CarWheelPosition.RearLeft, _wheelColliderRL);
        dictionary.Add(CarWheelPosition.RearRight, _wheelColliderRR);

        return dictionary;
    }

    WheelCollider[] GetFrontWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderFL,
            _wheelColliderFR
        };

        return wheelColliders;
    }

    WheelCollider[] GetRearWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderRL,
            _wheelColliderRR
        };

        return wheelColliders;
    }

    WheelCollider[] GetAllWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderFL,
            _wheelColliderFR,
            _wheelColliderRL,
            _wheelColliderRR
        };

        return wheelColliders;
    }
}
