using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", true);
        aim.currentFov = aim.aimFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Hip);
        }
    }
}