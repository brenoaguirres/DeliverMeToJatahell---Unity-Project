using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchscreen : MonoBehaviour
{
    private event Action touchDrag;
    [SerializeField] private Camera mainCamera;
    private Vector2 touchedPoint;
    public Vector3 cameraDirection;

    public void Touched()
    {
        if (Input.touchCount <= 0) return;

        touchedPoint = Input.GetTouch(0).position;
    }

    public void TouchDrag()
    {
        float horizontalValue = 0;
        float verticalValue = 0;

        if (Input.GetTouch(0).position.x > touchedPoint.x)
        {
            horizontalValue = 1;
        }
        else if (Input.GetTouch(0).position.x < touchedPoint.x)
        {
            Debug.Log("X Negative");
            horizontalValue = -1;
        }

        if (Input.GetTouch(0).position.y > touchedPoint.y)
        {
            Debug.Log("Y Positive");
            verticalValue = 1;
        }
        else if (Input.GetTouch(0).position.y < touchedPoint.y)
        {
            Debug.Log("Y Negative");
            verticalValue = -1;
        }

        cameraDirection = new Vector3(horizontalValue, verticalValue, 0);
    }
}
