using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 駆動方式. CarController での区別で使用する
/// </summary>
public enum CarDriveType
{
    /// <summary>
    /// 前輪駆動
    /// </summary>
    Front,
    /// <summary>
    /// 後輪駆動
    /// </summary>
    Rear,
    /// <summary>
    /// 四輪駆動
    /// </summary>
    FourWheelDrive
}
