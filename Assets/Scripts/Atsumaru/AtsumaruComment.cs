using RpgAtsumaruApiForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtsumaruComment
{
    private RpgAtsumaruComment _api;

    public AtsumaruComment()
    {
    }

    public void Initialize()
    {
        _api = RpgAtsumaruApi.CommentApi;
    }

    public void ChangeScene(string sceneName, bool reset)
    {
        _api.ChangeScene(sceneName, reset);
    }

    public void SetContext(string context)
    {
        _api.SetContext(context);
    }
}
