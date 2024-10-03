using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : SingletonBehaviour<Core>
{
    private bool _isDebugMode;

    public bool IsDebugMode
    {
        get
        {
            return _isDebugMode;
        }
    }

    protected override Core This
    {
        get
        {
            return this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        _isDebugMode = true;
#else
        _isDebugMode = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void AwakeChild()
    {
        DontDestroyOnLoad(gameObject);
    }
}
