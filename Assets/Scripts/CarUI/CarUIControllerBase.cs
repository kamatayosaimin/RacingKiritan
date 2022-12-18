using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarUIControllerBase : MonoBehaviour
{
    private int _memoriCount = MemoriMax;
    private int _redZoneIndex = 10;
    [SerializeField] private float _memoriTextOffset;
    [SerializeField] private float _memoriImageOffset;
    [SerializeField] private Color _memoriColorWhite = Color.white;
    [SerializeField] private Color _memoriColorRed = Color.white;
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
    [SerializeField] private Image _memoriImagePrefab;
    private Image[] _warningLampImages;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private CarSlider _brakeSlider;
    [SerializeField] private CarSlider _accelSlider;

    private const int MemoriMax = 15;

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

    protected virtual void AwakeChild()
    {
    }
}
