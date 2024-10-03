using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クルマの その他音声情報
/// </summary>
[System.Serializable]
public class CarSoundOtherData
{
    [SerializeField]
    [Tooltip("レブリミット音声の 回転数誤差")]
    private float _revLimitOffset = 100f;

    public float RevLimitOffset
    {
        get
        {
            return _revLimitOffset;
        }
    }
}
