using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSound, _turboSound, _revLimitSound;
    private AudioSource _tireFLSound, _tireFRSound, _tireRLSound, _tireRRSound, _buppiganPrefab;
    [SerializeField] private AudioSource[] _mufflerSounds;
    private CarSoundPitchData _pitchData;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitTireSounds(Dictionary<string, AudioSource> soundDictionary)
    {
        try
        {
            _tireFLSound = soundDictionary[CarWheelPosition.FL];
            _tireFRSound = soundDictionary[CarWheelPosition.FR];
            _tireRLSound = soundDictionary[CarWheelPosition.RL];
            _tireRRSound = soundDictionary[CarWheelPosition.RR];
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitClipData(CarSoundClipData clipData)
    {
        try
        {
            _engineSound.clip = clipData.EngineClip;

            _turboSound.clip = clipData.TurboClip;

            if (_revLimitSound)
                _revLimitSound.clip = clipData.RevLimitClip;

            _tireFLSound.clip = _tireFRSound.clip = _tireRLSound.clip = _tireRRSound.clip = clipData.SquealClip;

            _buppiganPrefab = clipData.BuppiganPrefab;

            for (int i = 0; i < _mufflerSounds.Length; i++)
                _mufflerSounds[i].clip = clipData.MufflerClip;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitPitchData(CarSoundPitchData pitchData)
    {
        try
        {
            _pitchData = pitchData;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }
}
