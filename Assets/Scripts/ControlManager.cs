using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance { get; private set; }
    MainControls _controls;
    public MainControls Controls => _controls;

    [SerializeField] GameObject _cursor;
    [SerializeField] SpriteRenderer _cursorSpriteRenderer;

    private bool _lastInputWasGamePad = false;

    public bool IsCursorVisible { get; private set; }

    public Vector3 MouseCursorWorldPos => _cursor.transform.position;
    public Vector3 MouseCursorScreenPos => Camera.main.WorldToScreenPoint(_cursor.transform.position);

    public float gamepadCursorSpeed = 10f;
    public float mouseSpeed = 10f;


    void Awake()
    {
        Instance = this;
        _controls = new MainControls();
        _controls.Enable();

        Cursor.visible = false;
        ShowCursor();
    }

    public void ShowCursor()
    {
        _cursor.transform.position = Vector2.zero;
        _cursor.SetActive(true);
        IsCursorVisible = true;
    }

    public void HideCursor()
    {
        _cursor.SetActive(false);
        IsCursorVisible = false;
    }

    void LateUpdate()
    {
        if (IsCursorVisible)
        {
            var mouseMovement = Mouse.current.delta.ReadValue();
            var controlsMovement = Controls.Gameplay.Movement.ReadValue<Vector2>();

            if (mouseMovement.magnitude > 0) _lastInputWasGamePad = false;
            if (controlsMovement.magnitude > 0) _lastInputWasGamePad = true;

            if (_lastInputWasGamePad)
                _cursor.transform.position += (Vector3)controlsMovement * gamepadCursorSpeed;
            else
                _cursor.transform.position += (Vector3)Mouse.current.delta.ReadValue() * mouseSpeed;

            var camera = Camera.main;
            Vector2 min = camera.ScreenToWorldPoint(camera.pixelRect.min);
            Vector2 max = camera.ScreenToWorldPoint(camera.pixelRect.max);

            var currentCursorPosition = _cursor.transform.position;
            _cursor.transform.position = new Vector2(Mathf.Min(max.x, currentCursorPosition.x), Mathf.Min(max.y, currentCursorPosition.y));
            currentCursorPosition = _cursor.transform.position;
            _cursor.transform.position = new Vector2(Mathf.Max(min.x, currentCursorPosition.x), Mathf.Max(min.y, currentCursorPosition.y));

            if (Controls.Gameplay.Click.IsPressed()) 
                _cursorSpriteRenderer.color = new Color(1f, 1f, 1f, 0.9f);
            else
                _cursorSpriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);
        }

        //_cursor.transform.position = Camera.main.ScreenToWorldPoint()

        //var controls = ControlManager.Instance._controls;

        ////Action button sample
        //if (controls.Gameplay.Action1.IsPressed()) Debug.Log("Action1"); //Q or Gamepad A
        //if (controls.Gameplay.Action2.IsPressed()) Debug.Log("Action2"); //E or Gamepad B
        //if (controls.Gameplay.Action3.IsPressed()) Debug.Log("Action3"); //R or Gamepad X
        //if (controls.Gameplay.Action4.IsPressed()) Debug.Log("Action4"); //F or Gamepad Y
        //if (Controls.Gameplay.SelectLeft.triggered) Debug.Log("Selected left");
        //if (Controls.Gameplay.SelectRight.triggered) Debug.Log("Selected right");

        ////Movment sample
        //var movement = controls.Gameplay.Movement.ReadValue<Vector2>();
        //if (movement.magnitude > 0) Debug.Log("Movement: " + movement); 
    }
}
