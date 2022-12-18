using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarInputBase : MonoBehaviour
{
    private float _motor;
    private float _brake;
    private CarInputState _inputState;
    private CarController _controller;

    public float InputMotor
    {
        get
        {
            return _motor;
        }
        set
        {
            _motor = value;
        }
    }

    public float CurrentMotor
    {
        get
        {
            switch (_inputState)
            {
                case CarInputState.Standby:
                    return 0f;
                case CarInputState.Playing:
                    return _motor;
                case CarInputState.Finished:
                    return 0f;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public float InputBrake
    {
        get
        {
            return _brake;
        }
        set
        {
            _brake = value;
        }
    }

    public float CurrentBrake
    {
        get
        {
            switch (_inputState)
            {
                case CarInputState.Standby:
                    return 1f;
                case CarInputState.Playing:
                    return _brake;
                case CarInputState.Finished:
                    return 0f;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public virtual float InputSteering
    {
        get
        {
            return 0f;
        }
        set
        {
        }
    }

    public float CurrentSteering
    {
        get
        {
            switch (_inputState)
            {
                case CarInputState.Standby:
                    return InputSteering;
                case CarInputState.Playing:
                    return InputSteering;
                case CarInputState.Finished:
                    return 0f;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public CarInputState InputState
    {
        get
        {
            return _inputState;
        }
        set
        {
            _inputState = value;
        }
    }

    protected CarController Controller
    {
        get
        {
            return _controller;
        }
    }

    void Awake()
    {
        try
        {
            _controller = GetComponent<CarController>();

            AwakeChild();
        }
        catch (System.Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    protected virtual void AwakeChild()
    {
    }
}
