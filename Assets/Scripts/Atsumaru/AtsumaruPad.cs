using RpgAtsumaruApiForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtsumaruPad
{
    private RpgAtsumaruController _api;

    public AtsumaruPad()
    {
    }

    public void Initialize()
    {
        _api = RpgAtsumaruApi.ControllerApi;
    }

    public void StartControllerListen()
    {
        _api = RpgAtsumaruApi.ControllerApi;

        _api.StartControllerListen();
    }

    public void Update()
    {
        _api.Update();
    }

    public float GetHorizontal()
    {
        return GetAxis(RpgAtsumaruInputKey.Left, RpgAtsumaruInputKey.Right);
    }

    public float GetVertical()
    {
        return GetAxis(RpgAtsumaruInputKey.Down, RpgAtsumaruInputKey.Up);
    }

    float GetAxis(RpgAtsumaruInputKey negativeKey, RpgAtsumaruInputKey positiveKey)
    {
        float axis = 0f;

        if (GetButton(negativeKey))
            axis -= 1f;

        if (GetButton(positiveKey))
            axis += 1f;

        return axis;
    }

    public bool GetButton(RpgAtsumaruInputKey key)
    {
        return _api.GetButton(key);
    }

    public bool GetButtonDown(RpgAtsumaruInputKey key)
    {
        return _api.GetButtonDown(key);
    }

    public bool GetButtonUp(RpgAtsumaruInputKey key)
    {
        return _api.GetButtonUp(key);
    }

    public Vector2 GetVelocity()
    {
        float x = GetHorizontal(),
            y = GetVertical();

        return new Vector2(x, y);
    }

    public Vector2 GetVelocityNormalized()
    {
        return GetVelocity().normalized;
    }
}
