using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinigameInteractable : MonoBehaviour
{
    SpriteRenderer _sprite;

    bool _highlighted;

    public enum minigame { snakes, cauldren, stars }
    public minigame minigameToPlay;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_highlighted && ControlManager.Instance.Controls.Gameplay.Action1.triggered)
        {
            switch (minigameToPlay)
            {
                case minigame.snakes:
                    TimelineController.Instance.PlaySnakes();
                    break;
                case minigame.cauldren:
                    break;
                case minigame.stars:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _sprite.color = Color.yellow;
            _highlighted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _sprite.color = Color.white;
            _highlighted = false;
        }
    }
}
