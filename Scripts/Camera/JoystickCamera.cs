using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoystickCamera : MonoBehaviour
{
    public Vector2 cameraMovementValue;

    private void Update()
    {
        Joystick joystick = GetComponent<Joystick>();

        cameraMovementValue = new Vector2(joystick.Horizontal, -joystick.Vertical);
    }
}
