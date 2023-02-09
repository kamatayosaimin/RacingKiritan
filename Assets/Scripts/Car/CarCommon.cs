using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarCommon
{
    public const int MeterMemoriCount = 15;
    public const float MeterMomoriScale = 1000f;
    public const float MaxRpm = MeterMemoriCount * MeterMomoriScale;
    /// <summary>
    /// 0.001396263
    /// </summary>
    public const float PowerRate = Mathf.PI * 2f / 4500f;
}
