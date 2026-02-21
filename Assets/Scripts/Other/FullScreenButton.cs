using UnityEngine;
using UnityEngine.EventSystems;

public class FullScreenButton : MonoBehaviour, IPointerDownHandler
{
    private bool _isFullScreen;
    [SerializeField] private string _messageStyle = "FULLSCREEN {0}";
    [SerializeField] private string _messageOff = "OFF";
    [SerializeField] private string _messageOn = "ON";
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private UnityEngine.UI.Image _image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isFullScreen = Screen.fullScreen;

        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.fullScreen == _isFullScreen)
            return;

        _isFullScreen = Screen.fullScreen;

        SetUI();
    }

    void SetUI()
    {
        _text.text = string.Format(_messageStyle, _isFullScreen ? _messageOn : _messageOff);

        _image.sprite = _isFullScreen ? _offSprite : _onSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isFullScreen = !Screen.fullScreen;

        Screen.fullScreen = _isFullScreen;

        SetUI();
    }
}
