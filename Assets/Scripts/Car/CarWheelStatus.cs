using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelStatus
{
    private float? _slipLevel;
    private WheelHit _hit;
    private AudioSource _sound;
    private Transform _tireTransform;
    private WheelCollider _collider;

    public float? SlipLevel
    {
        get
        {
            return _slipLevel;
        }
    }

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
        SetHit();
        SetSlipLevel();
        SetPose();
    }

    void SetHit()
    {
        WheelHit hit;

        _collider.GetGroundHit(out hit);

        _hit = hit;
    }

    void SetSlipLevel()
    {
        List<float> list = new List<float>();
        System.Action<float, float, float> arg = (slip, extremum, asymptote) =>
        {
            slip = Mathf.Abs(slip);

            if (slip < extremum)
                return;

            float t = Mathf.InverseLerp(extremum, asymptote, slip);

            list.Add(t);
        };

        WheelFrictionCurve forwardFriction = _collider.forwardFriction;
        WheelFrictionCurve sidewaysFriction = _collider.sidewaysFriction;

        arg(_hit.forwardSlip, forwardFriction.extremumSlip, forwardFriction.asymptoteSlip);
        arg(_hit.sidewaysSlip, sidewaysFriction.extremumSlip, sidewaysFriction.asymptoteSlip);

        _slipLevel = list.Count > 0 ? Mathf.Max(list.ToArray()) : null;
    }

    void SetPose()
    {
        Quaternion rotation;
        Vector3 position;

        _collider.GetWorldPose(out position, out rotation);

        _tireTransform.rotation = rotation;
        _tireTransform.position = position;
    }
}
