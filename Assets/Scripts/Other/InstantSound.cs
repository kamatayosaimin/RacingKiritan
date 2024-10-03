using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantSound : MonoBehaviour
{
    private AudioSource _soundSource;

    void Awake()
    {
        _soundSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_soundSource.isPlaying)
            Destroy(gameObject);
    }
}
