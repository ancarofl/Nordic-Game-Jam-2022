using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public static TimelineController Instance { get; private set; }

    int _currentScene = -1;
    [SerializeField] bool _disableIntroInEditor = true;

    public Timer timer;

    public int introScenePath;
    public int roomScenePath;
    public int snakeScenePath;
    public int cauldrenScenePath;
    public int starsScenePath;
    public int EndBadScenePath;
    public int EndGoodScenePath;

    enum gameState { first, second, third }
    gameState _gameState;

    public enum minigameState { locked, open, completed }
    public minigameState snakeGameState = minigameState.locked;
    public minigameState cauldrenGameState = minigameState.open;
    public minigameState starGameState = minigameState.locked;

    bool clickDisabled;

    void Awake()
    {
        Instance = this;

        LoadScene(introScenePath);
        timer.gameObject.SetActive(false);
    }

    void LoadScene(int scenePath)
    {
        Debug.Log("About to load scene: " + scenePath);

        if (ControlManager.Instance.Controls.Gameplay.Click.IsPressed())
        {
            ControlManager.Instance.Controls.Gameplay.Click.Disable();
            clickDisabled = true;
        }

        if (_currentScene != -1)
            SceneManager.UnloadSceneAsync(_currentScene);
        else
        {
            List<Scene> scenesToRemove = new List<Scene>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == "PreScene") continue;
                scenesToRemove.Add(scene);
            }

            foreach (var scene in scenesToRemove)
            {
                SceneManager.UnloadSceneAsync(scene.path);
            }
        }

        _currentScene = scenePath;
        SceneManager.LoadScene(_currentScene, LoadSceneMode.Additive);

    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (clickDisabled && ControlManager.Instance.Controls.Gameplay.UnClick.IsPressed())
        {
            ControlManager.Instance.Controls.Gameplay.Click.Enable();
            clickDisabled = false;
        }
    }

    public void PlaySnakes()
    {
        LoadScene(snakeScenePath);
    }
    public void PlayCauldren()
    {
        LoadScene(cauldrenScenePath);
    }

    public void PlayStars()
    {
        LoadScene(starsScenePath);
    }

    public void IntroFinished()
    {
        LoadScene(roomScenePath);
        timer.gameObject.SetActive(true);
    }

    public void EndSceneFinished()
    {
        _disableIntroInEditor = false;
        SceneManager.LoadScene(0);
    }

    public void MinigameFailed()
    {
        Destroy(timer);
        LoadScene(EndBadScenePath);
    }

    public void TimeRanOut()
    {
        LoadScene(EndBadScenePath);
    }

    public void ReloadCurrentMinigame()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name == "PreScene") continue;
            _currentScene = scene.buildIndex;
        }

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
                Destroy(timer);
                LoadScene(EndGoodScenePath);
                break;
        }
    }
}
