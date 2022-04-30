using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Dragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    public delegate void DragEndedDelegate(Dragger draggableObject);
    public DragEndedDelegate dragEndedCallback;
    private Vector3 _dragOffset;
    private Camera _cam;
    [SerializeField] private float _speed = 20;

    private RectTransform rectTransform;
    // POinter Events
    public void OnBeginDrag (PointerEventData eventData) {
        Debug.Log("Onbegindrag");
    }
    public void OnDrag (PointerEventData eventData) {
            Debug.Log("OnDrag");
    }
    public void OnEndDrag (PointerEventData eventData) {
        Debug.Log("OnEndDrag");

    }

    public void OnPointerDown (PointerEventData eventData) {
    }
     void Awake(){
        _cam = Camera.main;

    }
    private void OnMouseDown() {
        _dragOffset = transform.position - getMousePos();
    }
    void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, getMousePos() + _dragOffset, _speed + Time.deltaTime);
    }
    private void OnMouseUp() {
        dragEndedCallback?.Invoke(this);
    }
    Vector3 getMousePos(){
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition + _dragOffset);
        mousePos.z = 0;
        return mousePos;
    }
}
