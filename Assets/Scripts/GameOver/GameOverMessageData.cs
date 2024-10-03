using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameOverMessageData
{
    [Multiline][SerializeField] private string _message;
    [SerializeField] private GameOverSpeaker _speaker;
    [SerializeField] private AudioClip _clip;

    public string Message
    {
        get
        {
            return _message;
        }
    }

    public GameOverSpeaker Speaker
    {
        get
        {
            return _speaker;
        }
    }

    public AudioClip Clip
    {
        get
        {
            return _clip;
        }
    }
}
