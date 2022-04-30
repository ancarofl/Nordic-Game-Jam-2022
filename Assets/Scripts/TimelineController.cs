using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public static TimelineController Instance { get; private set; }

    string _currentScene = null;
    [SerializeField] bool _disableInEditor = true;

    public string introScenePath;
    public string roomScenePath;
    public string snakeScenePath;
    public string cauldrenScenePath;
    public string starsScenePath;

    void Awake()
    {
        Instance = this;
        LoadScene(introScenePath);
    }

    void LoadScene(string scenePath)
    {
        Debug.Log("About to load scene: " + scenePath);

        if (_disableInEditor && Application.isEditor)
            return;

        if (_currentScene != null)
            SceneManager.UnloadSceneAsync(_currentScene);

        _currentScene = scenePath;
        SceneManager.LoadScene(_currentScene, LoadSceneMode.Additive);

    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySnakes()
    {
        LoadScene(snakeScenePath);
    }

    public void IntroFinished()
    {
        LoadScene(roomScenePath);
    }

}
