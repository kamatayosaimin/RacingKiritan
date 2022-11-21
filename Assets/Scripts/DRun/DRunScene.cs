using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRunScene : SceneBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Camera _camera;
    [SerializeField] private CarController _carController;
    [SerializeField] private CarSoundContent _soundContent;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    protected override void StartChild()
    {
        try
        {
            _carController.InitSound(_soundContent.PlayerClipData, _soundContent.PitchData);
        }
        catch (System.Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void UpdateChild()
    {
    }
}
