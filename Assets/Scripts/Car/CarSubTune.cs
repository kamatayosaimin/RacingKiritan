using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特殊チューニング
/// </summary>
public enum CarSubTune
{
    None,
    /// <summary>
    /// ボルトオンターボ
    /// </summary>
    Turbo,
    /// <summary>
    /// エンジンスワップ
    /// </summary>
    EngineChange,
    /// <summary>
    /// チューンドタイヤ装備
    /// </summary>
    Tire,
    /// <summary>
    /// チューンドサスペンション装備
    /// </summary>
    Suspension,
    /// <summary>
    /// 軽量化
    /// </summary>
    WeightDown
}
