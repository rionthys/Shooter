using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    private Rigidbody[] _rbs;

    private void Start()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        SetKinematic(true);
    }

    public void TriggerRagdoll()
    {
        SetKinematic(false);
    }

    private void SetKinematic(bool value)
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = value;
        }
    }
}