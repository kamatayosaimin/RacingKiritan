using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarTuneData
{
    [Range(1f, CarCommon.MeterMemoriCount)]
    [SerializeField]
    [Tooltip("メーターの 目盛数")]
    private int _meterMemoriCount = CarCommon.MeterMemoriCount;
    [Range(0f, CarCommon.MaxRpm)]
    [SerializeField]
    [Tooltip("メーターの レッドゾーン")] private float _redZone;
    [SerializeField]
    [Tooltip("特殊チューニング")] private CarSubTune _subTune;
    [SerializeField]
    [Tooltip("トルク曲線")]
    [RangeCurve(0f, 0f, CarCommon.MaxRpm, 200f)]
    private AnimationCurve _engineTorqueCurve;

    public int MeterMemoriCount
    {
        get
        {
            return _meterMemoriCount;
        }
    }

    public float RedZone
    {
        get
        {
            return _redZone;
        }
    }

    public CarSubTune SubTune
    {
        get
        {
            return _subTune;
        }
    }

    public AnimationCurve EngineTorqueCurve
    {
        get
        {
            return _engineTorqueCurve;
        }
    }
}
