using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private int _shiftIndex = 1;
    private float _speed;
    private float _engineRpm;
    private float _engineRpmMin;
    private float _engineRpmMax;
    private float _brakeTorqueFront;
    private float _brakeTorqueRear;
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
            float inputMotor = _input.CurrentMotor;
            float velocity = _rigidbody.velocity.magnitude;
            WheelCollider[] frontWheelColliders = GetFrontWheelColliders();
            WheelCollider[] rearWheelColliders = GetRearWheelColliders();

            ChangeShift();

            _speed = velocity * CarCommon.SpeedToKmH;

            Reverse(inputMotor);

            _engineRpm = WheelRpmToEngineRpm();

            SetMotor(inputMotor, frontWheelColliders, rearWheelColliders);
            SetBrake(frontWheelColliders, rearWheelColliders);
            SetSteering(frontWheelColliders);
            SetDownForce(velocity);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            foreach (var wv in GetWheelValues())
            {
                Quaternion rotation;
                Vector3 position;
                Transform tire = wv.Tire;

                wv.Collider.GetWorldPose(out position, out rotation);

                tire.rotation = rotation;
                tire.position = position;
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitData(CarData data, int tuneLevel)
    {
        try
        {
            _brakeTorqueFront = data.BrakeTorqueFront;
            _brakeTorqueRear = data.BrakeTorqueRear;
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

            InitSubTune(data, tuneLevel);

            string log = string.Format("{0} : {1} kg", name, GetTotalMass());

            Debug.Log(log);

            _rigidbody.mass += carManager.DriverWeight;
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

    void InitSubTune(CarData data, int tuneLevel)
    {
        try
        {
            CarSubTune[] subTunes = data.GetSubTunes(tuneLevel);

            bool isEngineTuned = subTunes.Any(t => t == CarSubTune.Turbo || t == CarSubTune.EngineChange);

            _aspirationType = GetAspirationType(data.AspirationType, isEngineTuned);

            if (isEngineTuned)
                _rigidbody.mass += data.EngineTuneWeight;

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

    void ChangeShift()
    {
        try
        {
            if (_input.IsShiftDown && _input.IsShiftUp)
            {
                _input.IsShiftDown = _input.IsShiftUp = false;

                return;
            }

            if (_input.IsShiftDown)
            {
                _input.IsShiftDown = false;

                if (_shiftIndex > 1)
                    _shiftIndex--;
            }

            if (_input.IsShiftUp)
            {
                _input.IsShiftUp = false;

                if (_shiftIndex >= 1 && _shiftIndex < _gearRatio.Length - 1)
                    _shiftIndex++;
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void Reverse(float inputMotor)
    {
        try
        {
            if (_speed >= _reverseSpeedLimit)
                return;

            if (_shiftIndex > 0 && inputMotor < 0f)
                _shiftIndex = 0;

            if (_shiftIndex == 0 && inputMotor > 0f)
                _shiftIndex = 1;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetMotor(float inputMotor, WheelCollider[] frontWheelColliders, WheelCollider[] rearWheelColliders)
    {
        try
        {
            float motorTorque = GetMotorTorqueNM(inputMotor);

            switch (_driveType)
            {
                case CarDriveType.Front:
                    SetMotorTwoWD(motorTorque, frontWheelColliders);

                    break;
                case CarDriveType.Rear:
                    SetMotorTwoWD(motorTorque, rearWheelColliders);

                    break;
                case CarDriveType.FourWheelDrive:
                    SetMotorFourWD(motorTorque, frontWheelColliders, rearWheelColliders);

                    break;
                default:
                    throw new ArgumentException();
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetMotorTwoWD(float motorTorque, WheelCollider[] wheelColliders)
    {
        try
        {
            float torque = motorTorque / wheelColliders.Length;

            foreach (var wc in wheelColliders)
                wc.motorTorque = torque;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetMotorFourWD(float motorTorque, WheelCollider[] frontWheelColliders, WheelCollider[] rearWheelColliders)
    {
        try
        {
            float frontMotorTorque;
            float rearMotorTorque = motorTorque * _fourWDBalance;

            frontMotorTorque = motorTorque - rearMotorTorque;

            float frontTorque = frontMotorTorque / frontWheelColliders.Length;
            float rearTorque = rearMotorTorque / rearWheelColliders.Length;

            foreach (var wc in frontWheelColliders)
                wc.motorTorque = frontTorque;

            foreach (var wc in rearWheelColliders)
                wc.motorTorque = rearTorque;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetBrake(WheelCollider[] frontWheelColliders, WheelCollider[] rearWheelColliders)
    {
        try
        {
            float input = _input.CurrentBrake;
            float front = _brakeTorqueFront * input;
            float rear = _brakeTorqueRear * input;

            foreach (var wc in frontWheelColliders)
                wc.brakeTorque = front;

            foreach (var wc in rearWheelColliders)
                wc.brakeTorque = rear;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetSteering(WheelCollider[] frontWheelColliders)
    {
        try
        {
            float steering = _steering * _input.CurrentSteering;

            foreach (var wc in frontWheelColliders)
                wc.steerAngle = steering;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SetDownForce(float velocity)
    {
        try
        {
            Vector3 downForce = Vector3.down * (_downForce * velocity);

            _rigidbody.AddForce(downForce);
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
    /// 車速からエンジン rpm を算出
    /// </summary>
    /// <returns></returns>
    public float SpeedToEngineRpm()
    {
        float rpm = _speed * GetGearRatio() * _finalGear / _wheelRate;

        return GetEngineRpm(rpm);
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
        return _gearRatio[_shiftIndex];
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

    CarAspirationType GetAspirationType(CarAspirationType type, bool isTuned)
    {
        switch (type)
        {
            case CarAspirationType.NA:
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

    CarWheelDictionary<AudioSource> GetTireSoundDictionary()
    {
        return GetWheelColliderDictionary()
            .SelectDictionary(wc => wc.GetComponent<AudioSource>())
            .DictionaryCast<CarWheelDictionary<AudioSource>>();
    }

    public CarWheelDictionary<WheelCollider> GetWheelColliderDictionary()
    {
        CarWheelDictionary<WheelCollider> dictionary = new CarWheelDictionary<WheelCollider>();

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

    CarWheelValue[] GetWheelValues()
    {
        List<CarWheelValue> list = new List<CarWheelValue>();

        list.Add(new CarWheelValue(_wheelColliderFL, _tireFL));
        list.Add(new CarWheelValue(_wheelColliderFR, _tireFR));
        list.Add(new CarWheelValue(_wheelColliderRL, _tireRL));
        list.Add(new CarWheelValue(_wheelColliderRR, _tireRR));

        return list.ToArray();
    }
}
