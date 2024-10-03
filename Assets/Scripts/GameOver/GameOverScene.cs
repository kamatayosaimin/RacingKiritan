using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScene : SceneBehaviour
{
    private int _messageIndex;
    [SerializeField] private string _nextSceneName;
    [SerializeField] private string _kiritanName;
    [SerializeField] private string _manhuntName;
    [SerializeField] private string _animatorParamName;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _messageText;
    private Animator _animator;
    [SerializeField] private AudioSource _voiceSound;
    [SerializeField] private GameOverMessageData[] _messageDatas;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void NextMessage()
    {
        try
        {
            _messageIndex++;

            SetMessage();

            _animator.SetInteger(_animatorParamName, _messageIndex);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void AwakeChild()
    {
        try
        {
            _animator = GetComponent<Animator>();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void StartChild()
    {
        try
        {
            SetMessage();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void UpdateChild()
    {
    }

    void SetMessage()
    {
        try
        {
            if (_messageIndex >= _messageDatas.Length)
            {
                GetComponent<LoadController>().LoadScene(_nextSceneName);

                return;
            }

            _nameText.text = GetName(_messageDatas[_messageIndex].Speaker);

            _messageText.text = _messageDatas[_messageIndex].Message;

            _voiceSound.clip = _messageDatas[_messageIndex].Clip;

            _voiceSound.Play();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    string GetName(GameOverSpeaker speaker)
    {
        switch (speaker)
        {
            case GameOverSpeaker.None:
                return string.Empty;
            case GameOverSpeaker.Kiritan:
                return _kiritanName;
            case GameOverSpeaker.Manhunt:
                return _manhuntName;
            default:
                throw new ArgumentException();
        }
    }
}
