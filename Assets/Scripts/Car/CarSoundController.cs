using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSound, _turboSound, _revLimitSound;
    private AudioSource _tireFLSound, _tireFRSound, _tireRLSound, _tireRRSound, _buppiganPrefab;
    [SerializeField] private AudioSource[] _mufflerSounds;
    private CarSoundClipData _clipData;
    private CarSoundPitchData _pitchData;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitTireSounds(AudioSource fl, AudioSource fr, AudioSource rl, AudioSource rr)
    {
        _tireFLSound = fl;
        _tireFRSound = fr;
        _tireRLSound = rl;
        _tireRRSound = rr;
    }

    public void InitClipData(CarSoundClipData clipData)
    {
        _clipData = clipData;
    }

    public void InitPitchData(CarSoundPitchData pitchData)
    {
        _pitchData = pitchData;
    }
}
