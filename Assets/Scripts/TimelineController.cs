using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public static TimelineController Instance { get; private set; }

    [SerializeField] List<string> _scenePaths;
    int _sceneIndex = -1;
    string _currentScene = null;
    [SerializeField] bool _disableInEditor = true;

    void Awake()
    {
        Instance = this;

        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (_disableInEditor && Application.isEditor)
            return;

        if (_currentScene != null)
            SceneManager.UnloadSceneAsync(_currentScene);

        _sceneIndex++;
        if (_sceneIndex < _scenePaths.Count)
        {
            _currentScene = _scenePaths[_sceneIndex];
            SceneManager.LoadScene(_currentScene, LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntroFinished()
    {
        LoadNextScene();
    }

}
