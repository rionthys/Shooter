using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void EnterState(MovementManagerState movement);

    public abstract void UpdateState(MovementManagerState movement);
}