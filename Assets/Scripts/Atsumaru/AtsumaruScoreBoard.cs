using RpgAtsumaruApiForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtsumaruScoreBoard
{
    private RpgAtsumaruScoreboard _api;

    private static long _highScore;

    public AtsumaruScoreBoard()
    {
    }

    public void Initiaize()
    {
        _api = RpgAtsumaruApi.ScoreboardApi;
    }

    public async void ShowScoreBoard(long score)
    {
        if (score <= _highScore)
            return;

        _highScore = score;

        int boardId = 1;

        await _api.SendScoreAsync(boardId, _highScore);
        await _api.ShowScoreboardAsync(boardId);
    }
}
