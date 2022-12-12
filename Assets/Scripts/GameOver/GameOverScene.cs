using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScene : SceneBehaviour
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private string _kiritanName;
    [SerializeField] private string _manhuntName;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _messageText;
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

    protected override void StartChild()
    {
    }

    protected override void UpdateChild()
    {
    }
}
