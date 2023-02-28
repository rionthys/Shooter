using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementManagerState movement){ }

    public override void UpdateState(MovementManagerState movement)
    {
        MovementBaseState nextState = null;

        if (movement.direction.magnitude > 0.1f)
        {
            nextState = (Input.GetKey(KeyCode.LeftShift)) ? movement.Run : movement.Walk;
        }
        else if (Input.GetKeyDown(KeyCode.C)) 
        {
            nextState = movement.Crouch;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            nextState = movement.Jump;
        }

        if (nextState != null)
            movement.SwitchState(nextState);
    }
}