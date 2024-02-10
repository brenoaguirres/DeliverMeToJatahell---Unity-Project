using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for top-down camera [Outdated]

public class CameraController : MonoBehaviour
{
    #region Fields and Properties
    public GameObject Player;
    Vector3 compensationDistance;
    #endregion

    #region Execution
    void Start()
    {
        compensationDistance = transform.position - Player.transform.position;
    }


    void Update()
    {
        transform.position = Player.transform.position + compensationDistance;
    }
    #endregion
}
