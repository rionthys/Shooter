using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementManagerState movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementManagerState movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitToRunState(movement);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ExitToCrouchState(movement);
        }
        else if (movement.direction.magnitude < 0.1f)
        {
            ExitToIdleState(movement);
        }

        SetCurrentMoveSpeed(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitToJumpState(movement);
        }
    }

    private void ExitToRunState(MovementManagerState movement)
    {
        ExitState(movement, movement.Run);
    }

    private void ExitToCrouchState(MovementManagerState movement)
    {
        ExitState(movement, movement.Crouch);
    }

    private void ExitToIdleState(MovementManagerState movement)
    {
        ExitState(movement, movement.Idle);
    }

    private void ExitToJumpState(MovementManagerState movement)
    {
        movement.previousState = this;
        ExitState(movement, movement.Jump);
    }

    private void SetCurrentMoveSpeed(MovementManagerState movement)
    {
        movement.currentMoveSpeed = movement.vInput < 0 ? movement.walkBackSpeed : movement.walkSpeed;
    }

    private void ExitState(MovementManagerState movement, MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}