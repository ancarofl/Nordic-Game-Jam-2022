using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public GameObject couldron;
    public List<string> droppedIngredients;

    public List<string> winningRecipe;
    public List<AudioClip> winningSounds;
    public List<Color> winningColors;
    public static int currentIndex;
    private AudioSource sourceAudio;
    public float snapRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ControlManager.Instance.ShowCursor();

        DOTween.To(() => MusicManager.Instance.PianoVolume, (x) => MusicManager.Instance.PianoVolume = x, 0f, 3f).SetEase(Ease.InQuad);
        DOTween.To(() => MusicManager.Instance.DrumVolume, (x) => MusicManager.Instance.DrumVolume = x, 0f, 3f).SetEase(Ease.InQuad);
        DOTween.To(() => MusicManager.Instance.BassVolume, (x) => MusicManager.Instance.BassVolume = x, 0f, 3f).SetEase(Ease.InQuad);
        MusicManager.Instance.ShakerVolume = 0f;

        Dragger.dragEndedCallback = OnDragEnded;
        winningRecipe = new List<string> { "Claw", "Feather", "Boquet", "Rain" };
        sourceAudio = GetComponent<AudioSource>();
        currentIndex = 0;
        // play id 
        sourceAudio.clip = winningSounds[currentIndex];
        sourceAudio.Play();
    }

    private void OnDragEnded(Dragger draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.position, snapPoint.position);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition;
            // Snapped draggable properties
            // Debug.Log(draggable.name_ing);   
            var check = winningRecipe.IndexOf(draggable.name_ing);
            if (check == currentIndex)
            {

                // Right Ingredint
                FeedbackManager.Instance.DidGood();
                droppedIngredients.Add(draggable.name_ing);
                couldron.GetComponent<Renderer>().material.color = winningColors[currentIndex];
                currentIndex += 1;
                if (currentIndex == winningRecipe.Count)
                {
                    // WON
                    couldron.GetComponent<Renderer>().material.color = Color.green;
                    sourceAudio.Stop();
                    FeedbackManager.Instance.FinishedMiniGameGood();
                }
                else
                {
                    // keep playing, next audio
                    sourceAudio.clip = winningSounds[currentIndex];
                    sourceAudio.Play();
                }
            }
            else
            {
                // wrong ing
                FeedbackManager.Instance.DidBad();
            }

            // droppedIngredients.Add(draggable.name_ing);
            // destroy objects when dropped.
            Destroy(draggable.gameObject);
        }

    }
}
