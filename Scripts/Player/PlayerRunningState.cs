using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : State
{
    #region Fields and Properties

    [SerializeField]
    private float runningSpeed;

    #endregion

    #region Execution

    public override State RunCurrentState()
    {
        throw new System.NotImplementedException();
    }
    
    #endregion
}