using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public static TimelineController Instance { get; private set; }

    string _currentScene = null;
    [SerializeField] bool _disableInEditor = true;

    public Timer timer;

    public string introScenePath;
    public string roomScenePath;
    public string snakeScenePath;
    public string cauldrenScenePath;
    public string starsScenePath;
    public string EndBadScenePath;
    public string EndGoodScenePath;

    enum gameState { first, second, third }
    gameState _gameState;

    public enum minigameState { locked, open, completed }
    public minigameState snakeGameState = minigameState.locked;
    public minigameState cauldrenGameState = minigameState.open;
    public minigameState starGameState = minigameState.locked;

    void Awake()
    {
        Instance = this;
        LoadScene(introScenePath);
        if (!_disableInEditor)
            timer.gameObject.SetActive(false);
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
        timer.gameObject.SetActive(true);
    }

    public void EndSceneFinished()
    {
        SceneManager.LoadScene(0);
    }

    public void MinigameFailed()
    {
        LoadScene(EndBadScenePath);
    }

    public void TimeRanOut()
    {
        LoadScene(EndBadScenePath);
    }

    public void ReloadCurrentMinigame()
    {
        LoadScene(_currentScene);
    }

    public void ReturnFromMinigame()
    {
        switch (_gameState)
        {
            case gameState.first:
                _gameState = gameState.second;
                snakeGameState = minigameState.open;
                cauldrenGameState = minigameState.completed;
                starGameState = minigameState.locked;
                LoadScene(roomScenePath);
                break;
            case gameState.second:
                _gameState = gameState.third;
                snakeGameState = minigameState.completed;
                cauldrenGameState = minigameState.completed;
                starGameState = minigameState.open;
                LoadScene(roomScenePath);
                break;
            case gameState.third:
                snakeGameState = minigameState.completed;
                cauldrenGameState = minigameState.completed;
                starGameState = minigameState.completed;
                LoadScene(EndGoodScenePath);
                break;
        }
    }
}
