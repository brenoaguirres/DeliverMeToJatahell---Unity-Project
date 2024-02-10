using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transforms the asset in a billboard, making it always look at the camera

public class Billboard : MonoBehaviour
{
    #region Execution
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
    #endregion
}
