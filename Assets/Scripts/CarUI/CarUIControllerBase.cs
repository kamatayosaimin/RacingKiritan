using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarUIControllerBase : MonoBehaviour
{
    [SerializeField] private float _memoriTextOffset;
    [SerializeField] private float _memoriImageOffset;
    private float _meterScale;
    [SerializeField] private string _inputStyle = "0.000";
    [SerializeField] private Color _warningLampOffColor = Color.white;
    [SerializeField] private Color _warningLampOnColor = Color.white;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _shiftText;
    [SerializeField] private TextMeshProUGUI _brakeText;
    [SerializeField] private TextMeshProUGUI _accelText;
    [SerializeField] private TextMeshProUGUI _memoriTextPrefab;
    [SerializeField] private Gradient _speedGradient;
    [SerializeField] private RectTransform _memoriImageParent;
    [SerializeField] private RectTransform _memoriTextParent;
    [SerializeField] private RectTransform _warningLampParent;
    [SerializeField] private Image _meterImage;
    [SerializeField] private Image _redZoneImage;
    [SerializeField] private Image _memoriImagePrefab;
    private Image[] _warningLampImages;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private CarSlider _brakeSlider;
    [SerializeField] private CarSlider _accelSlider;

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
