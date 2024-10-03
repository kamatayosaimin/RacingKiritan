using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    [SerializeField] private LoadUI _uiPrefab;

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
        LoadUI ui = Instantiate(_uiPrefab);

        StartCoroutine(LoadState(load, ui));
    }

    public void Reload()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadState(AsyncOperation load, LoadUI ui)
    {
        while (true)
        {
            ui.SetSliderValue(load.progress);

            yield return null;
        }
    }
}
