using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBehaviour : MonoBehaviour
{
    [SerializeField] private bool _commentReset;
    private AtsumaruManager _atsumaruManager;

    public AtsumaruManager AtsumaruManager
    {
        get
        {
            return _atsumaruManager;
        }
    }

    protected abstract void StartChild();
    protected abstract void UpdateChild();

    void Awake()
    {
        _atsumaruManager = GetComponent<AtsumaruManager>();

        AwakeChild();
    }

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        _atsumaruManager.Comment.ChangeScene(sceneName, _commentReset);

        StartChild();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChild();
    }

    protected virtual void AwakeChild()
    {
    }
}
