using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DRunScene : SceneBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Camera _camera;
    [SerializeField] private CarController _carController;
    [SerializeField] private CarData _carData;
    private CarInputDRun _carInput;
    [SerializeField] private CarSoundContent _soundContent;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void OnSteering(InputAction.CallbackContext context)
    {
        Debug.Log($"Steering : {context.ReadValue<float>()}");
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        Debug.Log($"Accel : {context.ReadValue<float>()}");
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        Debug.Log($"Brake : {context.ReadValue<float>()}");
    }

    public void OnRear(InputAction.CallbackContext context)
    {
        Debug.Log($"Rear : {context.ReadValue<float>()}");
    }

    public void OnShiftDown(InputAction.CallbackContext context)
    {
        Debug.Log($"ShiftDown : {context.ReadValueAsButton()}");
    }

    public void OnShiftUp(InputAction.CallbackContext context)
    {
        Debug.Log($"ShiftUp : {context.ReadValueAsButton()}");
    }

    protected override void AwakeChild()
    {
        try
        {
            _carInput = _carController.gameObject.AddComponent<CarInputDRun>();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void StartChild()
    {
        try
        {
            _carController.InitData(_carData, 0);
            _carController.InitInput(_carInput);
            _carController.InitSound(_soundContent.PlayerClipData, _soundContent.PitchData);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void UpdateChild()
    {
    }
}
