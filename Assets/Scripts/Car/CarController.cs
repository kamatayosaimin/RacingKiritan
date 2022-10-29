using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private WheelCollider _wheelColliderFL, _wheelColliderFR, _wheelColliderRL, _wheelColliderRR;
    private CarSoundController _soundController;

    void Awake()
    {
        try
        {
            _rigidbody = GetComponent<Rigidbody>();
            _soundController = GetComponent<CarSoundController>();
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
    }

    public void InitSound(CarSoundClipData clipData, CarSoundPitchData pitchData)
    {
        try
        {
            if (!_soundController)
                return;

            _soundController.InitTireSounds(GetTireSoundDictionary());
            _soundController.InitClipData(clipData);
            _soundController.InitPitchData(pitchData);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public float GetTotalMass()
    {
        float mass = _rigidbody.mass;

        mass += GetAllWheelColliders().Sum(wc => wc.mass);

        return mass;
    }

    Dictionary<string, AudioSource> GetTireSoundDictionary()
    {
        return GetWheelColliderDictionary().ToDictionary(p => p.Key, p => p.Value.GetComponent<AudioSource>());
    }

    Dictionary<string, WheelCollider> GetWheelColliderDictionary()
    {
        Dictionary<string, WheelCollider> dictionary = new Dictionary<string, WheelCollider>();

        dictionary.Add(CarWheelPosition.FL, _wheelColliderFL);
        dictionary.Add(CarWheelPosition.FR, _wheelColliderFR);
        dictionary.Add(CarWheelPosition.RL, _wheelColliderRL);
        dictionary.Add(CarWheelPosition.RR, _wheelColliderRR);

        return dictionary;
    }

    WheelCollider[] GetFrontWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderFL,
            _wheelColliderFR
        };

        return wheelColliders;
    }

    WheelCollider[] GetRearWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderRL,
            _wheelColliderRR
        };

        return wheelColliders;
    }

    WheelCollider[] GetAllWheelColliders()
    {
        WheelCollider[] wheelColliders = new[]
        {
            _wheelColliderFL,
            _wheelColliderFR,
            _wheelColliderRL,
            _wheelColliderRR
        };

        return wheelColliders;
    }
}
