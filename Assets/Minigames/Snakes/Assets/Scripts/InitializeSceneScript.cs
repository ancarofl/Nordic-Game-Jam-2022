using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSceneScript : MonoBehaviour
{
    public GameObject downLeftHorizontalSnakePrefab;
    public GameObject downLeftVerticalSnakePrefab;
    public GameObject downRightHorizontalSnakePrefab;
    public GameObject downRightVerticalSnakePrefab;
    public GameObject upLeftHorizontalSnakePrefab;
    public GameObject upLeftVerticalSnakePrefab;
    public GameObject upRightHorizontalSnakePrefab;
    public GameObject upRightVerticalSnakePrefab;

    public int snakeNumber;
    public GameObject selectedSnake;

    private GameObject[] snakes;

    void Awake()
    {
        Debug.Log("Debug Snake - awake");

        snakes = new GameObject[Mathf.Max(snakeNumber, 6)];
        generateSnakes();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug Snake - start");
    }

    // Update is called once per frame
    void Update()
    {
        var controls = ControlManager.Instance.Controls;

        // Q
        if (controls.Gameplay.Action1.triggered)
        {
            Debug.Log("Q => Kill");
        }
        // E
        else if (controls.Gameplay.Action2.triggered)
        {
            Debug.Log("E => Pat");
        }
        // F. Change this to Action3 (R) in case the other minigames only use 3 buttons.
        else if (controls.Gameplay.Action4.triggered)
        {
            Debug.Log("F => Feed");
        }
    }

    void generateSnakes()
    {
        GameObject snake = Instantiate(downRightVerticalSnakePrefab);
        snake.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake.transform.position = new Vector2(-6.1f, -0.44f);
        snake.name = "Snake";
        snakes[0] = snake;
        selectedSnake = snake;

        GameObject snake1 = Instantiate(downRightVerticalSnakePrefab);
        snake1.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake1.transform.position = new Vector2(-4.86f, -1.61f);
        snake1.name = "Snake 1";
        snakes[1] = snake1;

        GameObject snake2 = Instantiate(upLeftVerticalSnakePrefab);
        snake2.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake2.transform.position = new Vector2(-3.15f, 1.43f);
        snake2.name = "Snake 2";
        snakes[2] = snake2;

        GameObject snake3 = Instantiate(downLeftVerticalSnakePrefab);
        snake3.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake3.transform.position = new Vector2(0.11f, -1.76f);
        snake3.name = "Snake 3";
        snakes[3] = snake3;

        GameObject snake4 = Instantiate(downLeftVerticalSnakePrefab);
        snake4.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake4.transform.position = new Vector2(2.2f, -0.47f);
        snake4.name = "Snake 4";
        snakes[4] = snake4;

        GameObject snake5 = Instantiate(downLeftHorizontalSnakePrefab);
        snake5.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake5.transform.position = new Vector2(6.42f, -3.96f);
        snake5.name = "Snake 5";
        snakes[5] = snake5;
    }

    Color getRandomColor()
    {
        int rn = Random.Range(0, 8);
        switch (rn)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            case 4:
                return Color.cyan;
            case 5:
                return Color.yellow;
            case 6:
                return Color.magenta;
            case 7:
                return new Color(1, 165 / 255f, 0);
            case 8:
                return new Color(107 / 255f, 142 / 255f, 35 / 255f);
            case 9:
                return new Color(107 / 255f, 142 / 255f, 35 / 255f);
        }
        return Color.green;
    }
}
