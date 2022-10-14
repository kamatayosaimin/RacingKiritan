using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSliderValue(float value)
    {
        _slider.value = value;
    }
}
