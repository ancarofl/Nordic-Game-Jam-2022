using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class StarGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;

    private int xStarAmount = 11;
    private int yStarAmount = 11;
    private float starSpace = .70f;
    private Star[,] stars;
    private float starScale = 2;
    private bool started = false;

    private List<(int x,int y)> orderedCoordinates = new List<(int x, int y)>()
        {
            (0,10),
            (0,9),
            (1,8),
            (1,7),
            (2,6),
            (2,5),
            (3,4),
            (3,3),
            (4,2),
            (4,1),
            (5,0),
            (6,1),
            (6,2),
            (7,3),
            (7,4),
            (8,5),
            (8,6),
            (9,7),
            (9,8),
            (10,9),
            (10,10),
            (9,9),
            (8,9),
            (7,8),
            (6,7),
            (5,6),
            (4,5),
            (3,5),
            (2,4),
            (1,3),
            (0,2),
            (1,2),
            (2,2),
            (3,2),
            (4,3),
            (5,3),
            (6,3),
            (7,2),
            (8,2),
            (9,2),
            (10,2),
            (9,3),
            (8,4),
            (7,5),
            (6,5),
            (5,6),
            (4,7),
            (3,8),
            (2,9),
            (1,9),
            (0,10)
        };

    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.MoveCursorToScreenSpace(new Vector2(0.1f, 0.1f));
        stars = new Star[xStarAmount,yStarAmount];
        for(int x = 0; x < xStarAmount; x++)
        {
            for (int y = 0; y < yStarAmount; y++)
            {
                stars[x, y] = Instantiate(starPrefab, transform).GetComponent<Star>();
                stars[x, y].transform.position = transform.position + new Vector3(x * starSpace - xStarAmount * starSpace / 2, y * starSpace - yStarAmount * starSpace / 2, 0) ;
                //stars[x, y].Lives = orderedCoordinates.FindAll(i => (i.x, i.y) == (x, y)).Count;
                stars[x, y].GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0, 1, 1, 1, 1, 1);
                stars[x, y].transform.localScale = new Vector3(starScale, starScale, starScale);
                stars[x, y].Coordinates = (x, y);
                stars[x, y].Grid = this;
            }
        }
        Random.InitState(101);
    }

    void Update()
    {
        if(started && !ControlManager.Instance.Controls.Gameplay.Click.IsPressed()) 
        {
            started = false;
            FeedbackManager.Instance.DidBad();
        }
    }
    public void CheckSelection((int x, int y) coords)
    {
        if (stars[coords.x, coords.y].Ready)
        {
            if (orderedCoordinates[0] == coords)
            {

                Debug.Log("YOOS:" + coords.x + "   " + coords.y + "  ");

                if (!started)
                {
                    started = true;
                }
                //stars[coords.x, coords.y].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                stars[coords.x, coords.y].Ready = false;
                stars[coords.x, coords.y].GetComponent<SpriteRenderer>().color = Random.ColorHSV(1, 1, 1, 1, 1, 1, 1, 1);
                orderedCoordinates.RemoveAt(0);
            }
            else
            {
                Debug.Log("OOF" + coords.x + "   " + coords.y);
                FeedbackManager.Instance.DidBad();
                TimelineController.Instance.ReloadCurrentMinigame();
            }
        }
    }
}
