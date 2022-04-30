using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.HideCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlManager.Instance.Controls.Gameplay.Action1.triggered)
        {
            TimelineController.Instance.IntroFinished();
        }
    }
}
