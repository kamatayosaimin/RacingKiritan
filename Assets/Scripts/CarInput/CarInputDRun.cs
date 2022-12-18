using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputDRun : CarInputPlayer
{
    private float _steering;

    public override float InputSteering
    {
        get
        {
            return _steering;
        }
        set
        {
            _steering = value;
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

    protected override void StartChild()
    {
    }

    protected override void UpdateChild()
    {
    }
}
