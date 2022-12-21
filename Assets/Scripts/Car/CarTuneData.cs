using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarTuneData
{
    [Range(1f, CarCommon.MeterMemoriCount)][SerializeField] private int _meterMemoriCount = CarCommon.MeterMemoriCount;
    [Range(0f, 15000f)][SerializeField] private float _redZone;
    [SerializeField] private CarSubTune _subTune;
    [SerializeField][RangeCurve(0f, 0f, 15000f, 200f)] private AnimationCurve _engineTorqueCurve;

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
