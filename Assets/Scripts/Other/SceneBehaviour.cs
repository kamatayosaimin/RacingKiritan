using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBehaviour : MonoBehaviour
{
    [SerializeField] private bool _commentReset;
    private AtsumaruManager _atsumaruManager;
    [SerializeField] private Core _corePrefab;

    protected virtual string SceneName
    {
        get
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }

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

        if (!Core.Instance)
            Instantiate(_corePrefab);

        AwakeChild();
    }

    // Start is called before the first frame update
    void Start()
    {
        _atsumaruManager.Comment.ChangeScene(SceneName, _commentReset);

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
