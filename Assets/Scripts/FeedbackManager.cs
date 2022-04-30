using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{

    public static FeedbackManager Instance { get; private set; }

    public AudioSource goodSound;
    public AudioSource badSound;
    public Timer timer;

    void Awake()
    {
        Instance = this;
    }

    public void DidGood()
    {
        goodSound.Play();
    }

    public void DidBad()
    {
        badSound.Play();
        timer.RemoveTime(10f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (ControlManager.Instance.Controls.Gameplay.Action1.triggered) DidGood();
        //if (ControlManager.Instance.Controls.Gameplay.Action2.triggered) DidBad();
    }
}
