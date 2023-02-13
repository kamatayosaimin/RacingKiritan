using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CarUIControllerBase : MonoBehaviour
{
    [SerializeField] private float _memoriTextOffset;
    [SerializeField] private float _memoriImageOffset;
    [SerializeField] private float _powerLineScale = 1f;
    [SerializeField] private float _torqueLineScale = 1f;
    [SerializeField] private float _rpmLineScale = 1f;
    [SerializeField] private float _lineRpmSpan = 100f;
    private float _meterScale;
    [SerializeField] private string _inputStyle = "0.000";
    [SerializeField] private string _powerFormat = "{0:#} PS / {1} rpm";
    [SerializeField] private string _torqueFormat = "{0:#.#} kgm / {1} rpm";
    [SerializeField] private Color _warningLampOffColor = Color.white;
    [SerializeField] private Color _warningLampOnColor = Color.white;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _shiftText;
    [SerializeField] private TextMeshProUGUI _brakeText;
    [SerializeField] private TextMeshProUGUI _accelText;
    [SerializeField] private TextMeshProUGUI _memoriTextPrefab;
    [SerializeField] private TextMeshProUGUI _powerText;
    [SerializeField] private TextMeshProUGUI _torqueText;
    [SerializeField] private Gradient _speedGradient;
    [SerializeField] private RectTransform _memoriImageParent;
    [SerializeField] private RectTransform _memoriTextParent;
    [SerializeField] private RectTransform _warningLampParent;
    [SerializeField] private RectTransform _rpmLineTransform;
    [SerializeField] private Image _meterImage;
    [SerializeField] private Image _redZoneImage;
    [SerializeField] private Image _memoriImagePrefab;
    private Image[] _warningLampImages;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private CarSlider _brakeSlider;
    [SerializeField] private CarSlider _accelSlider;
    [SerializeField] private UILine _powerLine;
    [SerializeField] private UILine _torqueLine;

    void Awake()
    {
        _warningLampImages = _warningLampParent.GetComponentsInChildren<Image>();

        AwakeChild();
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void Init(CarData data, int tuneLevel)
    {
        try
        {
            InitMeter(data, tuneLevel);
            InitEngineSpec(data, tuneLevel);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetAccelText(float value)
    {
        try
        {
            _accelText.text = GetInputString(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetBrakeText(float value)
    {
        try
        {
            _brakeText.text = GetInputString(value);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetAccelSliderValue(float value)
    {
        try
        {
            _accelSlider.value = value;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    public void SetBrakeSliderValue(float value)
    {
        try
        {
            _brakeSlider.value = value;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected virtual void AwakeChild()
    {
    }

    void InitMeter(CarData data, int tuneLevel)
    {
        try
        {
            int memoriCount = data.GetMeterMemoriCount(tuneLevel);

            _meterScale = memoriCount * CarCommon.MeterMomoriScale;

            foreach (Transform c in _memoriImageParent)
                Destroy(c.gameObject);

            foreach (Transform c in _memoriTextParent)
                Destroy(c.gameObject);

            for (int i = 0; i <= memoriCount; i++)
            {
                string name = i.ToString();
                Quaternion rotation = GetMemoriRotation(i, memoriCount);

                MemoriImageInstantiate(name, rotation);
                MemoriTextInstantiate(name, rotation);
            }

            float redZone = data.GetRedZone(tuneLevel);

            _redZoneImage.fillAmount = Mathf.InverseLerp(_meterScale, 0f, redZone) * 0.75f;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void InitEngineSpec(CarData data, int tuneLevel)
    {
        try
        {
            float maxPowerValue = 0f;
            float maxPowerRpm = 0f;
            float maxTorqueValue = 0f;
            float maxTorqueRpm = 0f;
            List<Vector2> powerPositionList = new List<Vector2>();
            List<Vector2> torquePositionList = new List<Vector2>();
            AnimationCurve torqueCurve = data.GetEngineTorqueCurve(tuneLevel);

            Keyframe[] keys = torqueCurve.keys;
            Action<float> arg = t =>
            {
                float torque = torqueCurve.Evaluate(t);
                Vector2 torquePosition = Vector2.zero;

                torquePosition.x = t * _rpmLineScale;
                torquePosition.y = torque * _torqueLineScale;

                torquePositionList.Add(torquePosition);

                if (torque > maxTorqueValue)
                {
                    maxTorqueValue = torque;
                    maxTorqueRpm = t;
                }

                //Torque * rpm * PowerRate(Mathf.PI * 2 / 4500)
                float power = torque * t * CarCommon.PowerRate;
                Vector2 powerPosition = Vector2.zero;

                powerPosition.x = torquePosition.x;
                powerPosition.y = power * _powerLineScale;

                powerPositionList.Add(powerPosition);

                if (power > maxPowerValue)
                {
                    maxPowerValue = power;
                    maxPowerRpm = t;
                }
            };

            for (float t = keys[0].time; t < keys[keys.Length - 1].time; t += _lineRpmSpan)
                arg(t);

            arg(keys[keys.Length - 1].time);

            _powerLine.Positions = powerPositionList.ToArray();

            _torqueLine.Positions = torquePositionList.ToArray();

            _powerText.text = string.Format(_powerFormat, maxPowerValue, maxPowerRpm);

            _torqueText.text = string.Format(_torqueFormat, maxTorqueValue, maxTorqueRpm);
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void MemoriImageInstantiate(string name, Quaternion rotation)
    {
        try
        {
            Image instance = Instantiate(_memoriImagePrefab, Vector3.zero, rotation, _memoriImageParent);

            instance.name = name;
            instance.rectTransform.anchoredPosition3D = rotation * Vector3.up * _memoriImageOffset;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void MemoriTextInstantiate(string name, Quaternion rotation)
    {
        try
        {
            TextMeshProUGUI instance = Instantiate(
                _memoriTextPrefab,
                Vector3.zero,
                Quaternion.identity,
                _memoriTextParent
                );

            instance.name = name;
            instance.text = name;
            instance.rectTransform.anchoredPosition3D = rotation * Vector3.up * _memoriTextOffset;
        }
        catch (Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    protected string GetInputString(float value)
    {
        return value.ToString(_inputStyle);
    }

    Quaternion GetMemoriRotation(int index, int count)
    {
        float t = index / (float)count;
        Vector3 eulerAngles = Vector3.zero;

        eulerAngles.z = Mathf.Lerp(180f, -90f, t);

        return Quaternion.Euler(eulerAngles);
    }
}
