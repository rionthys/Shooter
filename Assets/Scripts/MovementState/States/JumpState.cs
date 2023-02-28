using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementManagerState movement)
    {
        string trigger = movement.previousState == movement.Idle ? "IdleJump" : "RunJump";
        movement.anim.SetTrigger(trigger);
    }

    public override void UpdateState(MovementManagerState movement)
    {
        if (movement.jumped && movement.isGrounded)
        {
            movement.jumped = false;
            MovementBaseState nextState = movement.Idle;
            if (movement.hInput != 0 || movement.vInput != 0)
            {
                nextState = Input.GetKey(KeyCode.LeftShift) ? movement.Run : movement.Walk;
            }
            movement.SwitchState(nextState);
        }
    }
}