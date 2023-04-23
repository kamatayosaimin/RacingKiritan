using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    private float _revLimitRpm;
    [SerializeField] private AudioSource _engineSound;
    [SerializeField] private AudioSource _turboSound;
    [SerializeField] private AudioSource _revLimitSound;
    private AudioSource _buppiganPrefab;
    [SerializeField] private AudioSource[] _mufflerSounds;
    private CarController _carController;
    private CarSoundPitchData _pitchData;

    void Awake()
    {
        try
        {
            _carController = GetComponent<CarController>();

            _carController.OnWheelHitUpdated += WheelHitUpdated;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            float engineRpm = _carController.EngineRpm;

            _engineSound.pitch = engineRpm * _pitchData.EnginePitchMultipler;

            if (_carController.IsRevLimitSound)
            {
                bool isRevLimit = engineRpm >= _revLimitRpm;

                if (_revLimitSound.mute == isRevLimit)
                    _revLimitSound.mute = !isRevLimit;
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void OnDestroy()
    {
        try
        {
            if (_carController)
            {
                _carController.OnAccelOff -= AccelOff;
                _carController.OnWheelHitUpdated -= WheelHitUpdated;
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        try
        {
            Vector3[] contactPoints = collision.contacts.Select(c => c.point).ToArray();

            Vector3 position = VectorUtil.Average3(contactPoints);

            Instantiate(_buppiganPrefab, position, Quaternion.identity);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitClipData(CarSoundClipData clipData, CarWheelDictionary<CarWheelStatus> wheelStatusDictionary)
    {
        try
        {
            foreach (var status in wheelStatusDictionary.Values)
            {
                AudioSource sound = status.Sound;

                sound.clip = clipData.SquealClip;

                sound.Play();
            }

            _engineSound.clip = clipData.EngineClip;

            _engineSound.Play();

            _turboSound.clip = clipData.TurboClip;

            _revLimitSound.clip = clipData.RevLimitClip;

            if (_carController.IsRevLimitSound)
                _revLimitSound.Play();

            _buppiganPrefab = clipData.BuppiganPrefab;

            foreach (var s in _mufflerSounds)
                s.clip = clipData.MufflerClip;

            _carController.OnAccelOff += AccelOff;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void InitOtherData(CarSoundOtherData otherData, float engineRpmMax)
    {
        try
        {
            _revLimitRpm = engineRpmMax - otherData.RevLimitOffset;
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

            _revLimitSound.pitch = pitchData.RevvLimitPitch;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void AccelOff(CarAspirationType aspirationType)
    {
        try
        {
            if (aspirationType == CarAspirationType.Turbo)
                _turboSound.Play();

            foreach (var s in _mufflerSounds)
                s.Play();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void WheelHitUpdated(CarWheelDictionary<CarWheelStatus> wheelStatusDictionary)
    {
        try
        {
            foreach (var status in wheelStatusDictionary.Values)
            {
                float? slipLevel = status.SlipLevel;
                bool isSlipping = slipLevel.HasValue;
                AudioSource sound = status.Sound;

                if (sound.mute == isSlipping)
                    sound.mute = !isSlipping;

                if (isSlipping)
                    sound.pitch = _pitchData.GetSquealPitch(slipLevel.Value);
            }
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }
}
