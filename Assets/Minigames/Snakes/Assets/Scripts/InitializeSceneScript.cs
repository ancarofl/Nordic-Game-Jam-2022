using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSceneScript : MonoBehaviour
{
    // Prefabs for snake types and snake glows by type
    public GameObject downLeftHorizontalSnakePrefab;
    public GameObject downLeftHorizontalSnakeGlowPrefab;

    public GameObject downLeftVerticalSnakePrefab;
    public GameObject downLeftVerticalSnakeGlowPrefab;

    public GameObject downRightHorizontalSnakePrefab;
    public GameObject downRightHorizontalSnakeGlowPrefab;

    public GameObject downRightVerticalSnakePrefab;
    public GameObject downRightVerticalSnakeGlowPrefab;

    public GameObject upLeftHorizontalSnakePrefab;
    public GameObject upLeftHorizontalSnakeGlowPrefab;

    public GameObject upLeftVerticalSnakePrefab;
    public GameObject upLeftVerticalSnakeGlowPrefab;

    public GameObject upRightHorizontalSnakePrefab;
    public GameObject upRightHorizontalSnakeGlowPrefab;

    public GameObject upRightVerticalSnakePrefab;
    public GameObject upRightVerticalSnakeGlowPrefab;


    public int snakeNumber;

    private GameObject[] snakes;
    private GameObject selectedSnake;
    private GameObject selectedSnakeGlow;
    private int selectedSnakeIndex;

    void Awake()
    {
        //Debug.Log("Debug Snake - awake");

        snakes = new GameObject[Mathf.Max(snakeNumber, 6)];
        generateSnakes();
    }

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("Debug Snake - start");
    }

    // Update is called once per frame
    void Update()
    {
        var controls = ControlManager.Instance.Controls;

        // Left
        if (controls.Gameplay.SelectLeft.triggered)
        {
            //Debug.Log("LEFT");

            SelectPreviousSnake();
        }
        // Right
        if (controls.Gameplay.SelectRight.triggered)
        {
            //Debug.Log("RIGHT");

            SelectNextSnake();
        }
        // Q
        if (controls.Gameplay.Action1.triggered)
        {
            //Debug.Log("Q => Kill");
        }
        // E
        else if (controls.Gameplay.Action2.triggered)
        {
            //Debug.Log("E => Pat");
        }
        // F. Change this to Action3 (R) in case the other minigames only use 3 buttons.
        else if (controls.Gameplay.Action4.triggered)
        {
           // Debug.Log("F => Feed");
        }
    }

    void SelectPreviousSnake()
    {
        // If not first element of the array: index = previous index
        if (selectedSnakeIndex > 0)
        {
            selectedSnakeIndex--;
        }
        // Otherwise: index = last index
        else
        {
            selectedSnakeIndex = snakeNumber - 1;
        }
        selectedSnake = snakes[selectedSnakeIndex];
        OutlineSnake();
    }



    void SelectNextSnake()
    {
        // If not last element of the array: index = next index
        if (selectedSnakeIndex < snakeNumber - 1)
        {
            selectedSnakeIndex++;
        }
        // Otherwise: index = first index
        else
        {
            selectedSnakeIndex = 0;
        }
        selectedSnake = snakes[selectedSnakeIndex];
        OutlineSnake();
    }
    void OutlineSnake()
    {
        // Remove old outline
        Destroy(selectedSnakeGlow);

        // Instantiate current outline
        if (selectedSnake.transform.CompareTag("DLH"))
        {
            //Debug.Log("DLH");
            selectedSnakeGlow = Instantiate(downLeftHorizontalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x + 0.05f, selectedSnake.transform.position.y - 0.001f);
        }
        else if (selectedSnake.transform.CompareTag("DLV"))
        {
            //Debug.Log("DLV");
            selectedSnakeGlow = Instantiate(downLeftVerticalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x - 0.01f, selectedSnake.transform.position.y - 0.026f); ;
        }
        else if (selectedSnake.transform.CompareTag("DRH"))
        {
            //Debug.Log("DRH");
            selectedSnakeGlow = Instantiate(downRightHorizontalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x + 2.729289f, selectedSnake.transform.position.y + 0.2612587f);
        }
        else if (selectedSnake.transform.CompareTag("DRV"))
        {
            //Debug.Log("DRV");
            selectedSnakeGlow = Instantiate(downRightVerticalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x + 0.037f, selectedSnake.transform.position.y - 0.018f);
        }
        else if (selectedSnake.transform.CompareTag("ULH"))
        {
            //Debug.Log("ULH");
            selectedSnakeGlow = Instantiate(upLeftHorizontalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x + 0.05f, selectedSnake.transform.position.y - 0.01f);
        }
        else if (selectedSnake.transform.CompareTag("ULV"))
        {
            //Debug.Log("ULV");
            selectedSnakeGlow = Instantiate(upLeftVerticalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x - 0.01f, selectedSnake.transform.position.y + 0.037f);
        }
        else if (selectedSnake.transform.CompareTag("URH"))
        {
            //Debug.Log("URH");
            selectedSnakeGlow = Instantiate(upRightHorizontalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x + 2.785f, selectedSnake.transform.position.y - 0.15f);
        }
        else if (selectedSnake.transform.CompareTag("URV"))
        {
            //Debug.Log("URV");
            selectedSnakeGlow = Instantiate(upRightHorizontalSnakeGlowPrefab);
            selectedSnakeGlow.transform.position = new Vector2(selectedSnake.transform.position.x - 0.01f, selectedSnake.transform.position.y + 0.037f);
        }
        selectedSnakeGlow.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    void generateSnakes()
    {
        GameObject snake = Instantiate(downRightHorizontalSnakePrefab);
        snake.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake.transform.position = new Vector2(-6.1f, -0.44f);
        snake.name = "Snake";
        snakes[0] = snake;
        selectedSnakeIndex = 0;
        selectedSnake = snake;
        OutlineSnake();

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
                return Color.white;
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
