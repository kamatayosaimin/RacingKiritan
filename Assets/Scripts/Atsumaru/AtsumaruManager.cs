using RpgAtsumaruApiForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtsumaruManager : MonoBehaviour
{
    private AtsumaruComment _comment;
    private AtsumaruPad _pad;
    private AtsumaruScoreBoard _scoreBoard;

    public AtsumaruComment Comment
    {
        get
        {
            return _comment;
        }
    }

    public AtsumaruPad Pad
    {
        get
        {
            return _pad;
        }
    }

    public AtsumaruScoreBoard ScoreBoard
    {
        get
        {
            return _scoreBoard;
        }
    }

    void Awake()
    {
        _comment = new AtsumaruComment();
        _pad = new AtsumaruPad();
        _scoreBoard = new AtsumaruScoreBoard();

        if (RpgAtsumaruApi.Initialized)
        {
            _pad.Initialize();

            _comment.Initialize();

            _scoreBoard.Initiaize();

            return;
        }

        RpgAtsumaruApi.Initialize();

        _pad.StartControllerListen();

        _comment.Initialize();

        _scoreBoard.Initiaize();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
