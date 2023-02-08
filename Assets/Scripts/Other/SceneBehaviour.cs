using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBehaviour : MonoBehaviour
{
    [SerializeField] private bool _commentReset;
    [SerializeField] private Core _corePrefab;

    protected virtual string SceneName
    {
        get
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }

    protected abstract void StartChild();
    protected abstract void UpdateChild();

    void Awake()
    {
        if (!Core.Instance)
            Instantiate(_corePrefab);

        try
        {
            AwakeChild();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

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

    protected virtual void AwakeChild()
    {
    }
}
