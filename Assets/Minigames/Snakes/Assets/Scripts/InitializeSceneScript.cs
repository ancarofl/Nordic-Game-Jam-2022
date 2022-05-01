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

    /* Used colours trackers. */
    private int redSnakesCounter = 0;
    private int greenSnakesCounter = 0;
    private int blueSnakesCounter = 0;
    private int whiteSnakesCounter = 0;
    private int yellowSnakesCounter = 0;
    private int magentaSnakesCounter = 0;
    private int orangeSnakesCounter = 0;
    private int dirtyGreenSnakesCounter = 0;
    private int pinkSnakesCounter = 0;

    private string[] winningActions;
    private List<int> skipSnakes;

    void Awake()
    {
        //Debug.Log("Debug Snake - awake");

        /* TODO: Look into mouse click selection and reenable the cursor. */
        ControlManager.Instance.HideCursor();

        snakes = new GameObject[Mathf.Max(snakeNumber, 6)];
        winningActions = new string[Mathf.Max(snakeNumber, 6)];
        skipSnakes = new List<int>();
        GenerateSnakes();
        DetermineWinningActionsForSnakeConfiguration(); /* TODO: Rename? */
    }

    void DetermineWinningActionsForSnakeConfiguration()
    {
        Debug.Log("RED SNAKES: " + redSnakesCounter);
        Debug.Log("GREEN SNAKES: " + greenSnakesCounter);
        Debug.Log("BLUE SNAKES: " + blueSnakesCounter);
        Debug.Log("WHITE SNAKES: " + whiteSnakesCounter);
        Debug.Log("YELOOW SNAKES: " + yellowSnakesCounter);
        Debug.Log("MAGENTA SNAKES: " + magentaSnakesCounter);
        Debug.Log("ORANGE SNAKES: " + orangeSnakesCounter);
        Debug.Log("DIRTY GREEN SNAKES: " + dirtyGreenSnakesCounter);
        Debug.Log("PINK SNAKES: " + pinkSnakesCounter);

        /* 
         * Stripped down version of the (static) manual.
         * 
         * 
         * F = FEED
         * K = KILL
         * P = PET
         * S = SKIP
         * 
         * 
             1. Magenta > green? S 1/2 rounded up => WIN
             2. Red > each other colour? K all => WIN. TO DO: Do something regarding time.
             3. Green > each other colour? F P F P...
             4. Yellow > each other colour? P F P F...
             5. 6 different colours?
                    DLH: K
                    DLV: S
                    DRH: P
                    DRV: F
                    ULH: P
                    ULV: do same action as previous action
                    URH: S
                    URV: F OR K OR P OR S
	        6. 5 orange => LOSE ON EITHER ACTION
            7. Dirty green > each other colour? horziontal: P OR F, vertical: K
            8. 0 blue? F P F -> S... => WIN
            9. 3 pink ? S pink, F rest
        *
        *
        *
        */


        if (magentaSnakesCounter > greenSnakesCounter)
        {
            Debug.Log("1---S 1/2 rounded up");

            /* TODO: Round up the division! */
            int firstHalf = snakeNumber / 2;
            for (int i = 0; i < firstHalf; i++)
            {
                winningActions[i] = "SKIP";
            }
            for (int i = firstHalf; i < snakeNumber; i++)
            {
                winningActions[i] = "EMPTY";
            }
        }
        else if (
                 redSnakesCounter > greenSnakesCounter && redSnakesCounter > blueSnakesCounter && redSnakesCounter > whiteSnakesCounter && 
                 redSnakesCounter > yellowSnakesCounter && redSnakesCounter > magentaSnakesCounter && redSnakesCounter > orangeSnakesCounter &&
                 redSnakesCounter > dirtyGreenSnakesCounter && redSnakesCounter > pinkSnakesCounter
                )
        {
            Debug.Log("2---K ALL.");

            for (int i = 0; i < snakeNumber; i++)
            {
                winningActions[i] = "KILL";
            }
        }
        else if (
                 greenSnakesCounter > redSnakesCounter && greenSnakesCounter > blueSnakesCounter && greenSnakesCounter > whiteSnakesCounter &&
                 greenSnakesCounter > yellowSnakesCounter && greenSnakesCounter > magentaSnakesCounter && greenSnakesCounter > orangeSnakesCounter &&
                 greenSnakesCounter > dirtyGreenSnakesCounter && greenSnakesCounter > pinkSnakesCounter
                )
        {
            Debug.Log("3---F P F P...");

            for (int i = 0; i < snakeNumber; i++)
            {
                if ( i % 2 == 0 )
                {
                    winningActions[i] = "FEED";
                }
                else
                {
                    winningActions[i] = "PET";
                }
            }
        }
        else if (
                 yellowSnakesCounter > redSnakesCounter && yellowSnakesCounter > blueSnakesCounter && yellowSnakesCounter > whiteSnakesCounter &&
                 yellowSnakesCounter > greenSnakesCounter && yellowSnakesCounter > magentaSnakesCounter && yellowSnakesCounter > orangeSnakesCounter &&
                 yellowSnakesCounter > dirtyGreenSnakesCounter && yellowSnakesCounter > pinkSnakesCounter
                )
        {
            Debug.Log("4---P F P F...");

            for (int i = 0; i < snakeNumber; i++)
            {
                if (i % 2 == 0)
                {
                    winningActions[i] = "PET";
                }
                else
                {
                    winningActions[i] = "FEED";
                }
            }
        }
        /* else if ()
        {
            Debug.Log("5---WIP THIS ONE---DLH DRH DLV DRH ULH URH ULV URV");
        } */
        else if (orangeSnakesCounter == 5)
        {
            Debug.Log("6---LOSS AS SOON AS ANY ACTION IS TAKEN");
        }
        else if (
                 dirtyGreenSnakesCounter > redSnakesCounter && dirtyGreenSnakesCounter > blueSnakesCounter && dirtyGreenSnakesCounter > whiteSnakesCounter &&
                 dirtyGreenSnakesCounter > yellowSnakesCounter && dirtyGreenSnakesCounter > magentaSnakesCounter && dirtyGreenSnakesCounter > orangeSnakesCounter &&
                 dirtyGreenSnakesCounter > greenSnakesCounter && dirtyGreenSnakesCounter > pinkSnakesCounter
                )
        {
            Debug.Log("7---H: P OR F; V: K");

            for (int i = 0; i < snakeNumber; i++)
            {
                if (snakes[i].transform.CompareTag("DLV") || snakes[i].transform.CompareTag("DRV") || snakes[i].transform.CompareTag("ULV") || snakes[i].transform.CompareTag("URV"))
                {
                    winningActions[i] = "KILL";
                }
                else
                {
                    winningActions[i] = "FEEDPET";
                }
            }
        }
        else if (blueSnakesCounter == 0)
        {
            Debug.Log("8---F P F S...");

            winningActions[0] = "FEED";
            winningActions[1] = "PET";
            winningActions[2] = "FEED";

            for (int i = 0; i < 3; i++)
            {
               winningActions[i] = "SKIP";
            }
        }
        else if (pinkSnakesCounter == 3)
        {
            Debug.Log("9---S pink F rest.");

            for (int i = 0; i < snakeNumber; i++)
            {
                if (snakes[i].transform.GetComponent<SpriteRenderer>().color == new Color(240 / 255f, 218 / 255f, 245 / 255f))
                {
                    winningActions[i] = "SKIP";
                }
                else
                {
                    winningActions[i] = "FEED";
                }
            }
        }
        else
        {
            Debug.Log("10---DLH DRH DLV DRH ULH URH ULV URV");
            /*                     
                DLH: K        
                DLV: S URH: S
                DRH: P ULH: P
                DRV: F
                URV: F OR K OR P OR S

                ULV: do same action as previous action
            */
            for (int i = 0; i < snakeNumber; i++)
            {
                if (snakes[i].transform.CompareTag("DLV") || snakes[i].transform.CompareTag("URH"))
                {
                    winningActions[i] = "SKIP";
                }
                else if (snakes[i].transform.CompareTag("DRH") || snakes[i].transform.CompareTag("ULH"))
                {
                    winningActions[i] = "PET";
                }
                else if (snakes[i].transform.CompareTag("URV"))
                {
                    winningActions[i] = "ANY";
                }
                else if (snakes[i].transform.CompareTag("DRV"))
                {
                    winningActions[i] = "FEED";
                }
                else if (snakes[i].transform.CompareTag("DLH"))
                {
                    winningActions[i] = "KILL";
                }
            }
        }
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

        if (controls.Gameplay.SelectLeft.triggered)
        {
            //Debug.Log("LEFT");

            SelectPreviousSnake();
        }
        if (controls.Gameplay.SelectRight.triggered)
        {
            //Debug.Log("RIGHT");

            SelectNextSnake();
        }
        // Q
        if (controls.Gameplay.Action1.triggered)
        {
            Debug.Log("Q => KILL. This should have been a " + winningActions[selectedSnakeIndex]);

            DisableSnake();
        }
        // E
        else if (controls.Gameplay.Action2.triggered)
        {
            Debug.Log("E => PET. This should have been a " + winningActions[selectedSnakeIndex]);

            DisableSnake();
        }
        // R
        else if (controls.Gameplay.Action3.triggered)
        {
            Debug.Log("R => SKIP. This should have been a " + winningActions[selectedSnakeIndex]);

            DisableSnake();
        }
        // F
        else if (controls.Gameplay.Action4.triggered)
        {
            Debug.Log("F => FEED. This should have been a " + winningActions[selectedSnakeIndex]);

            DisableSnake();
        }

    }

    void DisableSnake()
    {
        selectedSnake.SetActive(false);
        skipSnakes.Add(selectedSnakeIndex);
        SelectNextSnake();
    }

    /* TODO: Improve code. */
    void SelectPreviousSnake()
    {
        // If not last element of the array: index = next index
        if (selectedSnakeIndex > 0)
        {
            int tempNextIndex = selectedSnakeIndex - 1;

            while (skipSnakes.Contains(tempNextIndex))
            {
                if (tempNextIndex > 0)
                {
                    tempNextIndex--;
                }
                else
                {
                    tempNextIndex = snakeNumber - 1;
                }
            }
            selectedSnakeIndex = tempNextIndex;
        }
        // Otherwise: index = last index
        else
        {
            int tempNextIndex = snakeNumber - 1;

            while (skipSnakes.Contains(tempNextIndex))
            {
                if (tempNextIndex > 0)
                {
                    tempNextIndex--;
                }
                else
                {
                    tempNextIndex = snakeNumber - 1;
                }
            }
            selectedSnakeIndex = tempNextIndex;
        }

        selectedSnake = snakes[selectedSnakeIndex];
        OutlineSnake();
    }


    /* TODO: I like spaghetti but improve this code anyway. */
    void SelectNextSnake()
    {
        // Check if all snakes disabled. Then player won.
        if (skipSnakes.Count == snakeNumber)
        {
            Debug.Log("GG WON");
            return;
        }

        // If not last element of the array: index = next index
        if (selectedSnakeIndex < snakeNumber - 1)
        {

            int tempNextIndex = selectedSnakeIndex + 1;

            while (skipSnakes.Contains(tempNextIndex))
            {
                if (tempNextIndex < snakeNumber - 1)
                {
                    tempNextIndex++;
                }
                else
                {
                    tempNextIndex = 0;
                }
            }
            selectedSnakeIndex = tempNextIndex;
        }
        // Otherwise: index = first index
        else
        {
            int tempNextIndex = 0;

            while (skipSnakes.Contains(tempNextIndex))
            {
                if (tempNextIndex < snakeNumber - 1)
                {
                    tempNextIndex++;
                }
                else
                {
                    tempNextIndex = 0;
                }
            }
            selectedSnakeIndex = tempNextIndex;
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

    void GenerateSnakes()
    {
        GameObject snake = Instantiate(downRightHorizontalSnakePrefab);
        snake.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake.transform.position = new Vector2(-6.1f, -0.44f);
        snake.name = "Snake";
        snakes[0] = snake;
        selectedSnakeIndex = 0;
        selectedSnake = snake;
        OutlineSnake();

        GameObject snake1 = Instantiate(downRightVerticalSnakePrefab);
        snake1.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake1.transform.position = new Vector2(-4.86f, -1.61f);
        snake1.name = "Snake 1";
        snakes[1] = snake1;

        GameObject snake2 = Instantiate(upLeftVerticalSnakePrefab);
        snake2.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake2.transform.position = new Vector2(-3.15f, 1.43f);
        snake2.name = "Snake 2";
        snakes[2] = snake2;

        GameObject snake3 = Instantiate(downLeftVerticalSnakePrefab);
        snake3.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake3.transform.position = new Vector2(0.11f, -1.76f);
        snake3.name = "Snake 3";
        snakes[3] = snake3;

        GameObject snake4 = Instantiate(downLeftVerticalSnakePrefab);
        snake4.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake4.transform.position = new Vector2(2.2f, -0.47f);
        snake4.name = "Snake 4";
        snakes[4] = snake4;

        GameObject snake5 = Instantiate(downLeftHorizontalSnakePrefab);
        snake5.GetComponent<SpriteRenderer>().color = GetRandomColor();
        snake5.transform.position = new Vector2(6.42f, -3.96f);
        snake5.name = "Snake 5";
        snakes[5] = snake5;
    }

    Color GetRandomColor()
    {
        int rn = Random.Range(0, 8);
        switch (rn)
        {
            case 1:
                redSnakesCounter++;
                return Color.red;
            case 2:
                greenSnakesCounter++;
                return Color.green;
            case 3:
                blueSnakesCounter++;
                return Color.blue;
            case 4:
                whiteSnakesCounter++;
                return Color.white;
            case 5:
                yellowSnakesCounter++;
                return Color.yellow;
            case 6:
                magentaSnakesCounter++;
                return Color.magenta;
            case 7:
                orangeSnakesCounter++;
                return new Color(1, 165 / 255f, 0); // orange
            case 8:
                dirtyGreenSnakesCounter++;
                return new Color(107 / 255f, 142 / 255f, 35 / 255f); // a dirty green
            case 9:
                pinkSnakesCounter++;
                return new Color(240 / 255f, 218 / 255f, 245 / 255f); // a light pink
        }
        greenSnakesCounter++;
        return Color.green;
    }
}
