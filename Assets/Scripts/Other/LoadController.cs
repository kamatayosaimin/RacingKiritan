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
        LoadUI uI = Instantiate(_uiPrefab);

        StartCoroutine(LoadState(load, uI));
    }

    public void Reload()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadState(AsyncOperation load, LoadUI uI)
    {
        while (true)
        {
            uI.SetSliderValue(load.progress);

            yield return null;
        }
    }
}
