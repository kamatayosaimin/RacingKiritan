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

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void Init(CarController controller)
    {
        _controller = controller;
    }
}
