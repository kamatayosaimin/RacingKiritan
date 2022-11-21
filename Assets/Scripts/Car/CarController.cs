using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _tireFL;
    [SerializeField] private Transform _tireFR;
    [SerializeField] private Transform _tireRL;
    [SerializeField] private Transform _tireRR;
    [SerializeField] private WheelCollider _wheelColliderFL;
    [SerializeField] private WheelCollider _wheelColliderFR;
    [SerializeField] private WheelCollider _wheelColliderRL;
    [SerializeField] private WheelCollider _wheelColliderRR;
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

    Dictionary<CarWheelPosition, AudioSource> GetTireSoundDictionary()
    {
        return GetWheelColliderDictionary().ToDictionary(p => p.Key, p => p.Value.GetComponent<AudioSource>());
    }

    Dictionary<CarWheelPosition, WheelCollider> GetWheelColliderDictionary()
    {
        Dictionary<CarWheelPosition, WheelCollider> dictionary = new Dictionary<CarWheelPosition, WheelCollider>();

        dictionary.Add(CarWheelPosition.FrontLeft, _wheelColliderFL);
        dictionary.Add(CarWheelPosition.FrontRight, _wheelColliderFR);
        dictionary.Add(CarWheelPosition.RearLeft, _wheelColliderRL);
        dictionary.Add(CarWheelPosition.RearRight, _wheelColliderRR);

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
