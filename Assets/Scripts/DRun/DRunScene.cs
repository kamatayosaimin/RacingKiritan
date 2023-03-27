using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DRunScene : SceneBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private Camera _camera;
    [SerializeField] private CarController _playerCarController;
    [SerializeField] private CarData _carData;
    private CarInputDRun _carInput;
    [SerializeField] private CarSoundContent _soundContent;
    private CarUIControllerDRun _uiController;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    void LateUpdate()
    {
        Transform cameraTransform = _camera.transform;
        Transform carTransform = _playerCarController.transform;

        cameraTransform.rotation = carTransform.rotation;
        cameraTransform.position = carTransform.position + cameraTransform.rotation * _cameraOffset;
    }

    public void SetAccel(float value)
    {
        try
        {
            _carInput.InputMotor = value;

            _uiController.SetAccelText(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetBrake(float value)
    {
        try
        {
            _carInput.InputBrake = value;

            _uiController.SetBrakeText(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetSteering(float value)
    {
        try
        {
            _carInput.InputSteering = value;

            _uiController.SetSteeringText(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetShiftDown()
    {
        try
        {
            _carInput.IsShiftDown = true;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetShiftUp()
    {
        try
        {
            _carInput.IsShiftUp = true;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnSteering(InputAction.CallbackContext context)
    {
        try
        {
            float value = context.ReadValue<float>();

            _uiController.SetSteeringSliderValue(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        try
        {
            float value = context.ReadValue<float>();

            _uiController.SetAccelSliderValue(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        try
        {
            float value = context.ReadValue<float>();

            _uiController.SetBrakeSliderValue(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        try
        {
            _carInput.InputMotor = -context.ReadValue<float>();
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnShiftDown(InputAction.CallbackContext context)
    {
        try
        {
            if (context.ReadValueAsButton())
                _carInput.IsShiftDown = true;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void OnShiftUp(InputAction.CallbackContext context)
    {
        try
        {
            if (context.ReadValueAsButton())
                _carInput.IsShiftUp = true;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected override void AwakeChild()
    {
        try
        {
            _carInput = _playerCarController.gameObject.AddComponent<CarInputDRun>();
            _uiController = GetComponent<CarUIControllerDRun>();
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
            _carInput.InputState = CarInputState.Playing;

            int tuneLevel = 0;
            CarSoundClipData clipData = _soundContent.PlayerClipData;
            CarSoundOtherData otherData = _soundContent.OtherData;
            CarSoundPitchData pitchData = _soundContent.PitchData;

            _playerCarController.InitData(_carData, tuneLevel);
            _playerCarController.InitInput(_carInput);
            _playerCarController.InitSound(clipData, otherData, pitchData);

            _uiController.Init(_playerCarController, _carData, tuneLevel);
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
