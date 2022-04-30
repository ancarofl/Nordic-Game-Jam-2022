using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public GameObject couldron;
    public List<string> droppedIngredients;

    public List<string> winningRecipe; 

    public float snapRange = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
            Dragger.dragEndedCallback = OnDragEnded;
            winningRecipe = new List<string>{"Carrot", "Eyeball"};
    }

   private void OnDragEnded(Dragger draggable){
       float closestDistance  = -1;
       Transform closestSnapPoint = null;

       foreach(Transform snapPoint in snapPoints){
           float currentDistance = Vector2.Distance(draggable.transform.position, snapPoint.position);
           if(closestSnapPoint == null || currentDistance < closestDistance){
               closestSnapPoint = snapPoint;
               closestDistance = currentDistance;
           }
       }

       if(closestSnapPoint !=null && closestDistance <= snapRange){
           draggable.transform.localPosition = closestSnapPoint.localPosition;
            // Snapped draggable properties
            Debug.Log(draggable.name_ing);            
            if(winningRecipe.IndexOf(draggable.name_ing) != -1){
                // Hello 
                droppedIngredients.Add(draggable.name_ing);
                if(droppedIngredients.Count == winningRecipe.Count){
                    // WON
                    couldron.GetComponent<Renderer>().material.color =  Color.green;
                }
            }

            // droppedIngredients.Add(draggable.name_ing);
            Destroy(draggable.gameObject);
       }

   }
}
