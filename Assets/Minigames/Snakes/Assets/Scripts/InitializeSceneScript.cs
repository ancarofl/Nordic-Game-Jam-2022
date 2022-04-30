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

    void Awake()
    {
        Debug.Log("Debug Snake - awake");

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
        
    }

    void generateSnakes()
    {
        GameObject snake = Instantiate(downRightVerticalSnakePrefab);
        snake.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake.transform.position = new Vector2(-6.1f, -0.44f);

        GameObject snake2 = Instantiate(downRightVerticalSnakePrefab);
        snake2.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake2.transform.position = new Vector2(-4.86f, -1.61f);

        GameObject snake3 = Instantiate(upLeftVerticalSnakePrefab);
        snake3.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake3.transform.position = new Vector2(-3.15f, 1.43f);

        GameObject snake4 = Instantiate(downLeftVerticalSnakePrefab);
        snake4.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake4.transform.position = new Vector2(0.11f, -1.76f);

        GameObject snake5 = Instantiate(downLeftVerticalSnakePrefab);
        snake5.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake5.transform.position = new Vector2(2.2f, -0.47f);

        GameObject snake6 = Instantiate(downLeftHorizontalSnakePrefab);
        snake6.GetComponent<SpriteRenderer>().color = getRandomColor();
        snake6.transform.position = new Vector2(6.42f, -3.96f);
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
