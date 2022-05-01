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
    public float clockTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddTime(float time)
    {
        timeLeft += time;
        timeLeft = Mathf.Clamp(timeLeft, 0, maxTime);
    }

    public void RemoveTime(float time)
    {
        timeLeft -= time;
        transform.DOComplete();
        transform.DOShakeScale(0.2f, 0.03f, 20);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MusicManager.Instance.ShakerVolume = 1f - timeLeft / (maxTime * 0.8f) + 0.2f;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            TimelineController.Instance.TimeRanOut();
            Destroy(gameObject);
        }
        _timerImage.fillAmount = timeLeft / maxTime;

        clockTimer -= Time.deltaTime;
        if (clockTimer <= 0f && timeLeft / maxTime <= 0.25f)
        {
            clockTimer = 1f;
            transform.DOComplete();
            transform.DOShakeScale(0.2f, 0.03f * (1f - timeLeft / (maxTime * 0.25f) + 0.75f), 20);
        }
    }
}
