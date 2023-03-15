using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "GOIS/CarData")]
public class CarData : ScriptableObject
{
    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("4WD の駆動配分 [0, 100]")]
    private int _fourWDBalance = 50;
    [Range(1f, 6000f)]
    [SerializeField]
    [Tooltip("ファイナルギア [1, 6000]")]
    private int _finalGear = 3000;
    [Range(1f, 6000f)]
    [SerializeField]
    [Tooltip("ギア比. 0 番は 後退ギア比 [1, 6000]")]
    private int[] _gearRatio = DefaultGearRatio;
    [SerializeField]
    [Tooltip("ブレーキトルク")]
    private float _brakeTorque;
    [SerializeField]
    [Tooltip("ステアリングの 最大角度")]
    private float _steering;
    [SerializeField]
    [Tooltip("ダウンフォース")]
    private float _downForce;
    [SerializeField]
    [Tooltip("ボルトオンターボ / エンジンスワップ時の 重量変化量")]
    private float _engineTuneWeight;
    [SerializeField]
    [Tooltip("軽量化時の 減少する重量")]
    private float _weightDownValue;
    [SerializeField]
    [Tooltip("サスペンションチューン時の WheelCollider.suspensionSpring.spring")]
    private float _tunedSusupensionSpring = 35000f;
    [SerializeField]
    [Tooltip("サスペンションチューン時の WheelCollider.suspensionSpring.damper")]
    private float _tunedSuspensionDamper = 4500f;
    [SerializeField]
    [Tooltip("排気量")]
    private float _displacement;
    [SerializeField]
    [Tooltip("車種の 名前")]
    private string _carName;
    [SerializeField]
    [Tooltip("初期の 過給形式")]
    private CarAspirationType _aspirationType;
    [SerializeField]
    [Tooltip("駆動方式")]
    private CarDriveTypeStatus _driveType;
    [SerializeField]
    [Tooltip("エンジン形式")]
    private CarEngineType _engineType;
    [SerializeField]
    [Tooltip("チューンレベル毎の チューン情報")]
    private CarTuneData[] _tuneDatas = new CarTuneData[21];
    [SerializeField]
    [Tooltip("タイヤチューン時の WheelCollider.forwardFriction")]
    private CarWheelFrictionData _tunedForwardFriction
        = CarWheelFrictionData.DefaultForwardFriction;
    [SerializeField]
    [Tooltip("タイヤチューン時の WheelCollider.sidewaysFriction")]
    private CarWheelFrictionData _tunedSidewaysFriction
        = CarWheelFrictionData.DefaultSidewaysFriction;

    public int FourWDBalance
    {
        get
        {
            return _fourWDBalance;
        }
    }

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

    public float Steering
    {
        get
        {
            return _steering;
        }
    }

    public float DownForce
    {
        get
        {
            return _downForce;
        }
    }

    public float EngineTuneWeight
    {
        get
        {
            return _engineTuneWeight;
        }
    }

    public float WeightDownValue
    {
        get
        {
            return _weightDownValue;
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

    public float Displacement
    {
        get
        {
            return _displacement;
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

    public CarDriveTypeStatus DriveType
    {
        get
        {
            return _driveType;
        }
    }

    public CarEngineType EngineType
    {
        get
        {
            return _engineType;
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
            int[] gearRetio = new int[7];

            for (int i = 0; i < gearRetio.Length; i++)
                gearRetio[i] = 3000;

            return gearRetio;
        }
    }

    public int GetMeterMemoriCount(int tuneLevel)
    {
        return _tuneDatas[tuneLevel].MeterMemoriCount;
    }

    public float GetRedZone(int tuneLevel)
    {
        return _tuneDatas[tuneLevel].RedZone;
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
