using UnityEngine;
using UnityEngine.EventSystems;

public class FullScreenButton : MonoBehaviour, IPointerDownHandler
{
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
        SetUI(Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetUI(bool isFullScreen)
    {
        _text.text = string.Format(_messageStyle, isFullScreen ? _messageOn : _messageOff);

        _image.sprite = isFullScreen ? _offSprite : _onSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bool isFullScreen = !Screen.fullScreen;

        Screen.fullScreen = isFullScreen;

        SetUI(isFullScreen);
    }
}
