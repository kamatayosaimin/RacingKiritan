using UnityEngine;

public class FpsManager : SingletonBehaviour<FpsManager>
{
    [SerializeField] private int _defaultTargetFrameRate = -1;
    private int _count;
    [SerializeField] private float _maxTime = 0.5f;
    private float _time;
    [SerializeField] private string _style = "0.000 FPS";
    [SerializeField] private TMPro.TextMeshProUGUI _text;

    protected override FpsManager This
    {
        get
        {
            return this;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = _defaultTargetFrameRate;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _count = 0;
        _time = 0f;

        SetFps(0f);
    }

    // Update is called once per frame
    void Update()
    {
        _count++;
        _time += Time.deltaTime;

        if (_time < _maxTime)
            return;

        float fps = _count / _time;

        SetFps(fps);

        _count = 0;
        _time = 0f;
    }
    public void SetTargetFrameRate(int targetFrameRate)
    {
        Application.targetFrameRate = targetFrameRate;
    }

    void SetFps(float fps)
    {
        _text.text = fps.ToString(_style);
    }
}
