using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementManagerState movement)
    {
        movement.anim.SetBool("Crouching", true);
    }

    public override void UpdateState(MovementManagerState movement)
    {
        MovementBaseState nextState = null;

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            nextState = movement.Run;
        }
        else if (!Input.GetKey(KeyCode.C))
        {
            if (movement.direction.magnitude < 0.1f) 
                nextState = movement.Idle;
            else 
                nextState = movement.Walk;
        }

        if (nextState != null)
            ExitState(movement, nextState);

        movement.currentMoveSpeed = (movement.vInput < 0) ? movement.crouchBackSpeed : movement.crouchSpeed;
    }

    private void ExitState(MovementManagerState movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}