using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSound", menuName = "GOIS/CarSound")]
public class CarSoundContent : ScriptableObject
{
    [SerializeField] private CarSoundClipData _playerClipData, _enemyClipData;
    [SerializeField] private CarSoundPitchData _pitchData;

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

    public CarSoundPitchData PitchData
    {
        get
        {
            return _pitchData;
        }
    }
}
