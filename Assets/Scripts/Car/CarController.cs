using System;
using System.Collections;
using System.Collections.Generic;
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

            AudioSource flSound = GetTireSound(_wheelColliderFL),
                frSound = GetTireSound(_wheelColliderFR),
                rlSound = GetTireSound(_wheelColliderRL),
                rrSound = GetTireSound(_wheelColliderRR);

            _soundController.InitTireSounds(flSound, frSound, rlSound, rrSound);
            _soundController.InitClipData(clipData);
            _soundController.InitPitchData(pitchData);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    AudioSource GetTireSound(WheelCollider wheelCollider)
    {
        return wheelCollider.GetComponent<AudioSource>();
    }
}
