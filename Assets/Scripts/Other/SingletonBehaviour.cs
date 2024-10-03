using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    protected abstract T This { get; }

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);

            return;
        }

        _instance = This;

        AwakeChild();
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
