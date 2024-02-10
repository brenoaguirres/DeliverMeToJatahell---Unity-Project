using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : State
{
    #region Fields and Properties

    [SerializeField]
    private float walkingSpeed;

    #endregion

    #region Execution

    public override State RunCurrentState()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
