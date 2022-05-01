using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private Star PrevStar;

    public (int x, int y) Coordinates { get; set; }
    public StarGrid Grid { get; set; }
    public bool Ready { get; set; } = true;
    
    private BoxCollider2D col;
    

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        ControlManager.Instance.ShowCursor();

    }

    // Update is called once per frame
    void Update()
    {
        if (ControlManager.Instance.Controls.Gameplay.Click.IsPressed())
        {
            if (col.OverlapPoint(ControlManager.Instance.MouseCursorWorldPos))
            {
                Grid.CheckSelection(Coordinates);
            }
            else
            {
                Ready = true;
            }
        }
        
    }
}
