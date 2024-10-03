using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : SingletonBehaviour<ErrorManager>
{
    private bool _isShowing;
    private List<Exception> _exceptionList;
    [SerializeField] private ErrorUI _uiPrefab;

    protected override ErrorManager This
    {
        get
        {
            return this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _isShowing = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void AwakeChild()
    {
        _exceptionList = new List<Exception>();
    }

    public void AddException(Exception exception)
    {
        _exceptionList.Add(exception);

        if (!_isShowing)
            StartCoroutine(ShowState());

        throw exception;
    }

    public void ShowEnd()
    {
        _isShowing = false;

        if (_exceptionList.Count > 0)
            StartCoroutine(ShowState());
    }

    IEnumerator ShowState()
    {
        _isShowing = true;

        yield return null;

        ErrorUI ui = Instantiate(_uiPrefab);

        ui.ShowUI(_exceptionList.ToArray());

        _exceptionList.Clear();
    }
}
