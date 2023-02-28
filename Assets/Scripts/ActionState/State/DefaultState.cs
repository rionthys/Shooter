using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public float scrollDirection;

    public override void EnterState(ActionStateManager actions) 
    {

    }


    public override void UpdateState(ActionStateManager actions)
    {
        actions.rHandAim.weight = Mathf.Lerp(actions.rHandAim.weight, 1, 10 * Time.deltaTime);
        if(actions.rHandAim.weight == 1) actions.lHandIK.weight = 1;

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.reloadState);
        }
        else if(Input.mouseScrollDelta.y != 0){
            scrollDirection = Input.mouseScrollDelta.y;
            actions.SwitchState(actions.swapState);
        }
    }

    bool CanReload(ActionStateManager actions)
    {
        if (actions.ammo.currentAmmo == actions.ammo.clipSize)
        { 
            return false;
        }
        if (actions.ammo.extraAmmo == 0){ 
            return false;
        }
        else{
            return true;
        }
    }
}
