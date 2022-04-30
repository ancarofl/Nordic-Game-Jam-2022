using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float maxTime = 3f * 60f;
    float timeLeft = 3f * 60f;
    public Image _timerImage;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddTime(float time)
    {
        timeLeft += time;
    }

    public void RemoveTime(float time)
    {
        timeLeft -= time;
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 0.2f);
    }

    public void StartTimer()
    {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            TimelineController.Instance.TimeRanOut();
        }
        _timerImage.fillAmount = timeLeft / maxTime;
    }
}
