using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSound;
    [SerializeField] private AudioSource _turboSound;
    [SerializeField] private AudioSource _revLimitSound;
    private AudioSource _tireFLSound;
    private AudioSource _tireFRSound;
    private AudioSource _tireRLSound;
    private AudioSource _tireRRSound;
    private AudioSource _buppiganPrefab;
    [SerializeField] private AudioSource[] _mufflerSounds;
    private CarController _carController;
    private CarSoundPitchData _pitchData;

    void Awake()
    {
        try
        {
            _carController = GetComponent<CarController>();
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
            _engineSound.pitch = _carController.EngineRpm * _pitchData.EnginePitchMultipler;
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

    public void InitTireSounds(CarWheelDictionary<AudioSource> soundDictionary)
    {
        try
        {
            _tireFLSound = soundDictionary[CarWheelPosition.FrontLeft];
            _tireFRSound = soundDictionary[CarWheelPosition.FrontRight];
            _tireRLSound = soundDictionary[CarWheelPosition.RearLeft];
            _tireRRSound = soundDictionary[CarWheelPosition.RearRight];
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

            _engineSound.Play();

            _turboSound.clip = clipData.TurboClip;

            if (_revLimitSound)
            {
                _revLimitSound.clip = clipData.RevLimitClip;

                _revLimitSound.Play();
            }

            _tireFLSound.clip = _tireFRSound.clip = _tireRLSound.clip = _tireRRSound.clip = clipData.SquealClip;

            _tireFLSound.Play();

            _tireFRSound.Play();

            _tireRLSound.Play();

            _tireRRSound.Play();

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
