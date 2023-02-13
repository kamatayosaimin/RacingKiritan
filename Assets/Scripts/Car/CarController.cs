using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private int _shiftIndex = 0;
    private float _speed;
    private float _engineRpm;
    private float _engineRpmMin;
    private float _engineRpmMax;
    private float _brakeTorque;
    private float _steering;
    private float _downForce;
    private float _fourWDBalance;
    private float _finalGear;
    private float _wheelRate;
    private float _reverseSpeedLimit;
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

    public int ShiftIndex
    {
        get
        {
            return _shiftIndex;
        }
    }

    public float Speed
    {
        get
        {
            return _speed;
        }
    }

    public float EngineRpm
    {
        get
        {
            return _engineRpm;
        }
    }

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

    void FixedUpdate()
    {
        try
        {
            if (_input.IsShiftDown && _input.IsShiftUp)
                _input.IsShiftDown = _input.IsShiftUp = false;
            else
            {
                if (_input.IsShiftDown)
                {
                    _input.IsShiftDown = false;

                    if (_shiftIndex > 0)
                        _shiftIndex--;
                }

                if (_input.IsShiftUp)
                {
                    _input.IsShiftUp = false;

                    if (_shiftIndex >= 0 && _shiftIndex < _gearRatio.Length - 1)
                        _shiftIndex++;
                }
            }

            float inputMotor = _input.CurrentMotor;
            float inputBrake = _input.CurrentBrake;
            float inputSteering = _input.CurrentSteering;
            float velocity = _rigidbody.velocity.magnitude;

            _speed = velocity * CarCommon.SpeedToKmH;

            if (_speed < _reverseSpeedLimit)
            {
                if (_shiftIndex >= 0 && inputMotor < 0f)
                    _shiftIndex = -1;

                if (_shiftIndex == -1 && inputMotor > 0f)
                    _shiftIndex = 0;
            }

            _engineRpm = WheelRpmToEngineRpm();

            float motorTorque = GetMotorTorqueNM(inputMotor);
            WheelCollider[] frontWheelColliders = GetFrontWheelColliders();
            WheelCollider[] rearWheelColliders = GetRearWheelColliders();

            switch (_driveType)
            {
                case CarDriveType.Front:
                    SetWheelMotorTwoWD(motorTorque, frontWheelColliders);

                    break;
                case CarDriveType.Rear:
                    SetWheelMotorTwoWD(motorTorque, rearWheelColliders);

                    break;
                case CarDriveType.FourWheelDrive:
                    float frontMotorTorque;
                    float rearMotorTorque = motorTorque * _fourWDBalance;

                    frontMotorTorque = motorTorque - rearMotorTorque;

                    float frontTorque = frontMotorTorque / frontWheelColliders.Length;
                    float rearTorque = rearMotorTorque / rearWheelColliders.Length;

                    foreach (var wc in frontWheelColliders)
                        wc.motorTorque = frontTorque;

                    foreach (var wc in rearWheelColliders)
                        wc.motorTorque = rearTorque;

                    break;
                default:
                    throw new ArgumentException();
            }

            float brakeTorque = _brakeTorque * inputBrake;
            WheelCollider[] allWheelColliders = GetAllWheelColliders();

            foreach (var wc in allWheelColliders)
                wc.brakeTorque = brakeTorque;

            float steering = _steering * inputSteering;

            foreach (var wc in frontWheelColliders)
                wc.steerAngle = steering;

            Vector3 downForce = Vector3.down * (_downForce * velocity);

            _rigidbody.AddForce(downForce);
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
            _downForce = data.DownForce;
            _fourWDBalance = data.FourWDBalance / 100f;
            _finalGear = data.FinalGear / 1000f;
            _wheelRate = GetWheelRate();
            _gearRatio = data.GearRatio.Select(r => r / 1000f).ToArray();
            _driveType = GetDriveType(data.DriveType);
            _engineTorqueCurve = data.GetEngineTorqueCurve(tuneLevel);

            Keyframe[] keys = _engineTorqueCurve.keys;

            _engineRpmMin = keys[0].time;
            _engineRpmMax = keys[keys.Length - 1].time;

            CarManager carManager = CarManager.Instance;

            _reverseSpeedLimit = carManager.ReverseSpeedLimit;

            _rigidbody.mass += carManager.DriverWeight;

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
        try
        {
            _input = input;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
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

    void SetWheelMotorTwoWD(float motorTorque, WheelCollider[] wheelColliders)
    {
        float torque = motorTorque / wheelColliders.Length;

        foreach (var wc in wheelColliders)
            wc.motorTorque = torque;
    }

    public float GetTotalMass()
    {
        float mass = _rigidbody.mass;

        mass += GetAllWheelColliders().Sum(wc => wc.mass);

        return mass;
    }

    /// <summary>
    /// WheelCollider の平均 rpm からエンジン rpm を算出
    /// </summary>
    /// <returns></returns>
    float WheelRpmToEngineRpm()
    {
        float rpm = GetWheelRpmAverage() * GetGearRatio() * _finalGear;

        return GetEngineRpm(rpm);
    }

    /// <summary>
    /// 車速からエンジン rpm を算出
    /// </summary>
    /// <returns></returns>
    float SpeedToEngineRpm()
    {
        float rpm = _speed * GetGearRatio() * _finalGear / _wheelRate;

        return GetEngineRpm(rpm);
    }

    float GetWheelRpmAverage()
    {
        Func<WheelCollider, float> selector = wc => Mathf.Abs(wc.rpm);

        return GetAllWheelColliders().Average(selector);
    }

    float GetEngineRpm(float rpm)
    {
        return Mathf.Max(rpm, _engineRpmMin);
    }

    /// <summary>
    /// 軸トルク. 単位は N・m
    /// </summary>
    /// <param name="inputMotor"></param>
    /// <returns></returns>
    float GetMotorTorqueNM(float inputMotor)
    {
        return GetMotorTorqueKgM(inputMotor) * CarCommon.NMToKgM;
    }

    /// <summary>
    /// 軸トルク. 単位は kg・m
    /// </summary>
    /// <param name="inputMotor"></param>
    /// <returns></returns>
    float GetMotorTorqueKgM(float inputMotor)
    {
        return GetEngineTorque(inputMotor) * GetGearRatio() * _finalGear;
    }

    /// <summary>
    /// エンジントルク
    /// </summary>
    /// <param name="inputMotor"></param>
    /// <returns></returns>
    float GetEngineTorque(float inputMotor)
    {
        return _engineRpm > _engineRpmMax ? 0f : _engineTorqueCurve.Evaluate(_engineRpm) * inputMotor;
    }

    float GetGearRatio()
    {
        return _shiftIndex >= 0 ? _gearRatio[_shiftIndex] : _gearRatio[0];
    }

    /// <summary>
    /// タイヤ円周 * 単位調整係数
    /// </summary>
    /// <returns></returns>
    float GetWheelRate()
    {
        float radiusAverage = GetAllWheelColliders().Average(wc => wc.radius);

        return Mathf.PI * radiusAverage * CarCommon.WheelRateMultiple;
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
