using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クルマの 入力状態
/// </summary>
public enum CarInputState
{
    /// <summary>
    /// スタートカウント時
    /// </summary>
    Standby,
    /// <summary>
    /// プレイ時
    /// </summary>
    Playing,
    /// <summary>
    /// 終了時
    /// </summary>
    Finished
}
