using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSound", menuName = "GOIS/CarSound")]
public class CarSoundContent : ScriptableObject
{
    [SerializeField][Tooltip("プレイヤー用 AudioCilp 情報")] private CarSoundClipData _playerClipData;
    [SerializeField][Tooltip("ライバル用 AudioCilp 情報")] private CarSoundClipData _enemyClipData;
    [SerializeField][Tooltip("その他情報")] private CarSoundOtherData _otherData;
    [SerializeField][Tooltip("Pitch 情報")] private CarSoundPitchData _pitchData;

    public CarSoundClipData PlayerClipData
    {
        get
        {
            return _playerClipData;
        }
    }

    public CarSoundClipData EnemyClipData
    {
        get
        {
            return _enemyClipData;
        }
    }

    public CarSoundOtherData OtherData
    {
        get
        {
            return _otherData;
        }
    }

    public CarSoundPitchData PitchData
    {
        get
        {
            return _pitchData;
        }
    }
}
