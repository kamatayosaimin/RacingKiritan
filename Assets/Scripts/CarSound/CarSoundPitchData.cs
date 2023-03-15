using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クルマの 音声 Pitch 情報
/// </summary>
[System.Serializable]
public class CarSoundPitchData
{
    [SerializeField]
    [Tooltip("エンジン音係数")]
    private float _enginePitchMultipler;
    [SerializeField]
    [Tooltip("スキール音 Extremum 時係数")]
    private float _squealExtremumPitch;
    [SerializeField]
    [Tooltip("スキール音 Asymptote 時係数")]
    private float _squealAsymptotePitch;

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
