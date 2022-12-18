using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarInputPlayer : CarInputBase
{
    protected abstract void StartChild();
    protected abstract void UpdateChild();

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            StartChild();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            UpdateChild();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }
}
