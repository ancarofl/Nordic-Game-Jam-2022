using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGoodScene : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.HideCursor();
        MusicManager.Instance.BassVolume = 0;
        MusicManager.Instance.DrumVolume = 0;
        MusicManager.Instance.PianoVolume = 0;
        MusicManager.Instance.ShakerVolume = 0;
        text.DOFade(0, 10f);
        bg.transform.DOScale(1.08f, 10f).SetLoops(-1, LoopType.Yoyo);
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
