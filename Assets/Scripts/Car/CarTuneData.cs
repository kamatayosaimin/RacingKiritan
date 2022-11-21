using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarTuneData
{
    [SerializeField] private CarSubTune _subTune;
    [SerializeField][RangeCurve(0f, 0f, 15000f, 200f)] private AnimationCurve _engineTorqueCurve;

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
