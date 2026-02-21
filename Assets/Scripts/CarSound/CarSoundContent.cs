using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSound", menuName = "GOIS/CarSound")]
public class CarSoundContent : ScriptableObject
{
    [SerializeField]
    [Tooltip("Driver 別 AudioCilp 情報")]
    private CarSoundClipData[] _clipDatas;
    [SerializeField]
    [Tooltip("その他情報")]
    private CarSoundOtherData _otherData;
    [SerializeField]
    [Tooltip("Pitch 情報")]
    private CarSoundPitchData _pitchData;

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

    public CarSoundClipData GetClipData(CarDriverType driverType)
    {
        return _clipDatas.FirstOrDefault(d => d.DriverType == driverType);
    }
}
