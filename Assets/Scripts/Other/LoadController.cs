using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
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

    public void LoadScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        gameObject.SetActive(true);

        StartCoroutine(LoadState(load));
    }

    public void Reload()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadState(AsyncOperation load)
    {
        while (true)
        {
            _slider.value = load.progress;

            yield return null;
        }
    }
}
