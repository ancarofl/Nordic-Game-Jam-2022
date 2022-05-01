using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.HideCursor();
        MusicManager.Instance.BassVolume = 0f;
        MusicManager.Instance.PianoVolume = 0f;
        MusicManager.Instance.DrumVolume = 0f;
        MusicManager.Instance.ShakerVolume = 0f;

        DOTween.To(() => MusicManager.Instance.PianoVolume, (x) => MusicManager.Instance.PianoVolume = x, 1f, 3f).SetEase(Ease.InQuad);
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
