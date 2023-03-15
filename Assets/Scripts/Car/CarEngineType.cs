using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンジン形式
/// </summary>
public enum CarEngineType
{
    /// <summary>
    /// L3(直列 3 気筒)
    /// </summary>
    Inline3,
    /// <summary>
    /// L4(直列 4 気筒)
    /// </summary>
    Inline4,
    /// <summary>
    /// L6(直列 6 気筒)
    /// </summary>
    Inline6,
    /// <summary>
    /// V6(V 型 6 気筒)
    /// </summary>
    VType6,
    /// <summary>
    /// V8(V 型 8 気筒)
    /// </summary>
    VType8,
    /// <summary>
    /// V10(V 型 10 気筒)
    /// </summary>
    VType10,
    /// <summary>
    /// V12(V 型 12 気筒)
    /// </summary>
    VType12,
    /// <summary>
    /// F4(水平対向 4 気筒)
    /// </summary>
    Flat4,
    /// <summary>
    /// F6(水平対向 6 気筒)
    /// </summary>
    Flat6,
    /// <summary>
    /// R2(2 ローター)
    /// </summary>
    Rotary2,
    /// <summary>
    /// R3(3 ローター)
    /// </summary>
    Rotary3,
    /// <summary>
    /// L4D(直列 4 気筒ディーゼル)
    /// </summary>
    Inline4Diesel
}
