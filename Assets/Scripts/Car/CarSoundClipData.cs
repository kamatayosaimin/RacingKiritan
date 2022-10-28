using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarSoundClipData
{
    [SerializeField] private AudioClip _engineClip, _turboClip, _squealClip, _mufflerClip;
    [SerializeField] private AudioSource _buppiganPrefab;

    public AudioClip EngineClip
    {
        get
        {
            return _engineClip;
        }
    }

    public AudioClip TurboClip
    {
        get
        {
            return _turboClip;
        }
    }

    public AudioClip SquealClip
    {
        get
        {
            return _squealClip;
        }
    }

    public AudioClip MufflerClip
    {
        get
        {
            return _mufflerClip;
        }
    }

    public AudioSource BuppiganPrefab
    {
        get
        {
            return _buppiganPrefab;
        }
    }
}
