using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarCommon
{
    /// <summary>
    /// 60sec * 60min / 1000km
    /// </summary>
    public const float SpeedToKmH = 3.6f;
    /// <summary>
    /// �g���N�� N�Em ���� kg�Em �֕ϊ�
    /// </summary>
    public const float NMToKgM = 9.806652f;
    /// <summary>
    /// �z�C�[�����a(2) * �P�ʒ����W��(60 / 1000)
    /// </summary>
    public const float WheelRateMultiple = 0.12f;
    public const int MeterMemoriCount = 15;
    public const float MeterMomoriScale = 1000f;
    public const float MaxRpm = MeterMemoriCount * MeterMomoriScale;
    /// <summary>
    /// 0.001396263
    /// </summary>
    public const float PowerRate = Mathf.PI * 2f / 4500f;
}
