using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorUI : MonoBehaviour
{
    private int _index;
    private Exception[] _exceptions;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _messageText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowUI(Exception[] exceptions)
    {
        _index = 0;
        _exceptions = exceptions;

        Show();
    }

    public void BackError()
    {
        _index--;

        if (_index < 0)
            _index = _exceptions.Length - 1;

        Show();
    }

    public void NextError()
    {
        _index++;

        if (_index >= _exceptions.Length)
            _index = 0;

        Show();
    }

    public void Exit()
    {
        ErrorManager.Instance.ShowEnd();

        Destroy(gameObject);
    }

    void Show()
    {
        _headerText.text = $"ERROR {_index + 1} / {_exceptions.Length}";

        _messageText.text = _exceptions[_index].ToString();
    }
}
