using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelStatus
{
    private WheelHit _hit;
    private AudioSource _sound;
    private Transform _tireTransform;
    private WheelCollider _collider;

    public WheelHit Hit
    {
        get
        {
            return _hit;
        }
    }

    public AudioSource Sound
    {
        get
        {
            return _sound;
        }
    }

    public WheelCollider Collider
    {
        get
        {
            return _collider;
        }
    }

    public CarWheelStatus(WheelCollider collider, Transform tireTransform)
    {
        _sound = collider.GetComponent<AudioSource>();
        _tireTransform = tireTransform;
        _collider = collider;
    }

    public void Update()
    {
        WheelHit hit;

        _collider.GetGroundHit(out hit);

        _hit = hit;

        Quaternion rotation;
        Vector3 position;

        _collider.GetWorldPose(out position, out rotation);

        _tireTransform.rotation = rotation;
        _tireTransform.position = position;
    }
}
