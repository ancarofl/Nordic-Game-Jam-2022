using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dragger : MonoBehaviour
{
    public delegate void DragEndedDelegate(Dragger draggableObject);
    public static DragEndedDelegate dragEndedCallback;
    private Vector3 _dragOffset;
    private Camera _cam;
    public float volume=0.5f;

    [SerializeField] private float _speed = 20;
    [SerializeField] public string name_ing = "default_ingredient";
    public AudioSource ingSound;
    private Collider2D _collShape;

    private bool isBeingDragged;

    private static bool SomethingIsBeingDragged;

    private RectTransform rectTransform;
    // POinter Events
    // public void OnBeginDrag(PointerEventData eventData) {
    //     Debug.Log("Onbegindrag");
    // }
    // public void OnDrag(PointerEventData eventData) {
    //     Debug.Log("OnDrag");
    // }
    // public void OnEndDrag(PointerEventData eventData) {
    //     Debug.Log("OnEndDrag");
    // }

    // public void OnPointerDown (PointerEventData eventData) {
    // }
     void Awake(){
        _cam = Camera.main;
        _collShape = GetComponent<Collider2D>();
    }
    private void OnMouseDown() {
        // _dragOffset = transform.position - getMousePos();
        Debug.Log("Clicked");
                // ingSound.PlayOneShot(clip, volume);

    }
    void OnMouseDrag()
    {
        // transform.position = Vector3.MoveTowards(transform.position, getMousePos() + _dragOffset, _speed + Time.deltaTime);
    }
    private void OnMouseUp() {
        dragEndedCallback?.Invoke(this);
    }

    void Update()
    {
        var controlsManager = ControlManager.Instance;
        
        // controlsManager.MouseCursorWorldPos;

        if(controlsManager.Controls.Gameplay.Click.triggered && _collShape.OverlapPoint(controlsManager.MouseCursorWorldPos) && (!SomethingIsBeingDragged || isBeingDragged)){
            isBeingDragged = !isBeingDragged;
            if(!isBeingDragged){
                dragEndedCallback?.Invoke(this);
                SomethingIsBeingDragged = false;
            }
            else{
                SomethingIsBeingDragged = true;
                // Play ing audio
                ingSound.Play();

            }
        }
        
        if(isBeingDragged){
            transform.position = Vector3.MoveTowards(transform.position, controlsManager.MouseCursorWorldPos + _dragOffset, _speed + Time.deltaTime);
        }
        
        // if(_collShape.OverlapPoint(controlsManager.MouseCursorWorldPos)){
        //     Debug.Log("HELLLO");
        // }

        Debug.Log(controlsManager.Controls.Gameplay.Click.triggered);  
        Debug.Log(controlsManager.Controls.Gameplay.Click.IsPressed());

    }
    
    
}
