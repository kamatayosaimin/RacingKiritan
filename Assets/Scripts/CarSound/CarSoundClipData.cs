using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クルマの AudioClip
/// </summary>
[System.Serializable]
public class CarSoundClipData
{
    [SerializeField]
    [Tooltip("Driver")]
    private CarDriverType _driverType;
    [SerializeField]
    [Tooltip("エンジン音")]
    private AudioClip _engineClip;
    [SerializeField]
    [Tooltip("ターボ音")]
    private AudioClip _turboClip;
    [SerializeField]
    [Tooltip("レブリミット音")]
    private AudioClip _revLimitClip;
    [SerializeField]
    [Tooltip("スキール音")]
    private AudioClip _squealClip;
    [SerializeField]
    [Tooltip("マフラー音")]
    private AudioClip _mufflerClip;
    [SerializeField]
    [Tooltip("ブッピガン Prefab")]
    private AudioSource _buppiganPrefab;

    public CarDriverType DriverType
    {
        get
        {
            return _driverType;
        }
    }

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

    public AudioClip RevLimitClip
    {
        get
        {
            return _revLimitClip;
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
