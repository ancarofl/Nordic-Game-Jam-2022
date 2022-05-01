using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBadScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.HideCursor();
        MusicManager.Instance.BassVolume = 0;
        MusicManager.Instance.DrumVolume = 0;
        MusicManager.Instance.PianoVolume = 0;
        MusicManager.Instance.ShakerVolume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlManager.Instance.Controls.Gameplay.Action1.triggered)
        {
            TimelineController.Instance.EndSceneFinished();
        }
    }
}
