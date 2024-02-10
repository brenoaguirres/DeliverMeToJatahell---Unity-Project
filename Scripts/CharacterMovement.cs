using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage logic to move a generic character

public class CharacterMovement : MonoBehaviour
{
    #region Fields and Properties
    private Rigidbody characterRB;
    #endregion

    #region Initialization
    private void Awake()
    {
        characterRB = GetComponent<Rigidbody>();
    }
    #endregion

    #region Methods
    public void Movement(Vector3 direction, float speed)
    {
        characterRB.MovePosition(characterRB.position + (direction.normalized * speed * Time.deltaTime));
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
        characterRB.MoveRotation(rot);
    }

    public void Death()
    {
        characterRB.isKinematic = false;
        characterRB.constraints = RigidbodyConstraints.None;
        characterRB.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
    #endregion
}
