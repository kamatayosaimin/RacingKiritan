using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 駆動方式. データ表示用
/// </summary>
public enum CarDriveTypeStatus
{
    /// <summary>
    /// FF(Front engine Front drive)
    /// </summary>
    FF,
    /// <summary>
    /// FR(Front engine Rear drive)
    /// </summary>
    FR,
    /// <summary>
    /// MR(Midship engine Rear drive)
    /// </summary>
    MR,
    /// <summary>
    /// RR(Rear engine Rear drive)
    /// </summary>
    RR,
    /// <summary>
    /// 4WD(4 Wheel Drive)
    /// </summary>
    FourWD
}
