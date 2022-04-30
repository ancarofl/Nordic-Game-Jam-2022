using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance { get; private set; }
    MainControls _controls;
    public MainControls Controls => _controls;

    void Awake()
    {
        Instance = this;
        _controls = new MainControls();
        _controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controls.Gameplay.Action1.IsPressed()) Debug.Log("Action1");
        if (Controls.Gameplay.Action2.IsPressed()) Debug.Log("Action2");
        if (Controls.Gameplay.Action3.IsPressed()) Debug.Log("Action3");
        if (Controls.Gameplay.Action4.IsPressed()) Debug.Log("Action4");
        var movement = Controls.Gameplay.Movement.ReadValue<Vector2>();
        if (movement.magnitude > 0) Debug.Log("Movement: " + movement);
    }
}
