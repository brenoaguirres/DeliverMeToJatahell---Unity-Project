using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public float rotationDamping;

    public void RotatePlayer(Vector3 direction)
    {
        Rotate(direction);
    }

    public void RotatePlayer(LayerMask GroundMask)
    {
        float rayDebugSize = 100;
        RaycastHit impact;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayDebugSize, Color.red);

        if (Physics.Raycast(ray, out impact, rayDebugSize, GroundMask))
        {
            Vector3 playerTargetPosition = impact.point - transform.position;
            playerTargetPosition.y = transform.position.y;

            Rotate(playerTargetPosition);
        }
    }

    public void FaceMovementDirection()
    {
        var rot = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 
                Camera.main.transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rot,
            Time.deltaTime * rotationDamping
        );
    }
}
