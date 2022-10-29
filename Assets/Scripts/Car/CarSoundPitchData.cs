using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarSoundPitchData
{
    [SerializeField] private float _enginePitchMultipler;
    [SerializeField] private float _squealExtremumPitch;
    [SerializeField] private float _squealAsymptotePitch;

    public float EnginePitchMultipler
    {
        get
        {
            return _enginePitchMultipler;
        }
    }

    public float GetSquealPitch(float slip, float extremum, float asymptote)
    {
        slip = Mathf.Abs(slip);

        float t = Mathf.InverseLerp(slip, extremum, asymptote);

        return Mathf.Lerp(_squealExtremumPitch, _squealAsymptotePitch, t);
    }
}
