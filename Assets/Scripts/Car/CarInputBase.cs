using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarInputBase : MonoBehaviour
{
    private float _motor;
    private float _brake;
    private CarController _controller;

    public float Motor
    {
        get
        {
            return _motor;
        }
        protected set
        {
            _motor = value;
        }
    }

    public float Brake
    {
        get
        {
            return _brake;
        }
        protected set
        {
            _brake = value;
        }
    }

    public virtual float Steering
    {
        get
        {
            return 0f;
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
