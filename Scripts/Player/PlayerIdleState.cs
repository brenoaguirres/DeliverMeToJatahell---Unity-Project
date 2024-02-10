using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    #region Fields and Properties

    private AnimationsPlayer animations;

    // States for change
    [SerializeField]
    private PlayerWalkingState walk;
    [SerializeField]
    private PlayerRunningState run;

    #endregion

    #region Execution

    private void Awake() 
    {
        animations = GetComponentInParent<AnimationsPlayer>();
    }

    public override State RunCurrentState()
    {
        //if player is not moving
        //play idle animation and return idle state
        if (movementController.direction <= 0.1f)
        {
            animations.CheckIdle();
            return this;
        }
        else
        {
            //if running is on
            //return run state, else return walk state
            if ()
            {
                animations.UncheckIdle();
                return run;
            }
            else
            {
                animations.UncheckIdle();
                return walk;
            }
        }
    }

    #endregion
}
