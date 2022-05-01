using DG.Tweening;
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

    public Sprite spriteWhenDone;

    TimelineController.minigameState state = TimelineController.minigameState.open;
    Tweener shakeTween;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        switch (minigameToPlay)
        {
            case minigame.snakes:
                state = TimelineController.Instance.snakeGameState;
                break;
            case minigame.cauldren:
                state = TimelineController.Instance.cauldrenGameState;
                break;
            case minigame.stars:
                state = TimelineController.Instance.starGameState;
                break;
        }

        if (state == TimelineController.minigameState.completed)
        {
            _sprite.color = Color.white;
            _sprite.sprite = spriteWhenDone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == TimelineController.minigameState.completed)
            return;

        if (_highlighted && ControlManager.Instance.Controls.Gameplay.Action1.triggered)
        {
            if (state == TimelineController.minigameState.open)
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
            else if (state == TimelineController.minigameState.locked)
            {
                FeedbackManager.Instance.DidBad();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == TimelineController.minigameState.completed)
            return;

        if (collision.gameObject.tag == "Player")
        {
            _sprite.color = Color.yellow;
            _highlighted = true;
            shakeTween.Complete();
            shakeTween = transform.DOShakeRotation(1, new Vector3(0, 0, 1f), 10, 90, false).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (state == TimelineController.minigameState.completed)
            return;

        if (collision.gameObject.tag == "Player")
        {
            _sprite.color = Color.white;
            _highlighted = false;
            shakeTween.Kill();
            shakeTween = transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }
}
