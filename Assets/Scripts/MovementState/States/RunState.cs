using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementManagerState movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementManagerState movement)
    {
        MovementBaseState nextState = this;

        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            nextState = movement.Walk;
        } 
        else if (movement.direction.magnitude < 0.1f) 
        {
            nextState = movement.Idle;
        }

        if (movement.vInput < 0) 
        {
            movement.currentMoveSpeed = movement.runBackSpeed;
        } 
        else 
        {
            movement.currentMoveSpeed = movement.runSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            nextState = movement.Jump;
        }

        ExitState(movement, nextState);
    }

    private void ExitState(MovementManagerState movement, MovementBaseState state)
    {
        if (state != this)
        {
            movement.anim.SetBool("Running", false);
            movement.SwitchState(state);
        }
    }
}
