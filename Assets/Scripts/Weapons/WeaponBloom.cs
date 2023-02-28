using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    float defaultBloomAngle = 3;

    [SerializeField, Range(0, 10)]
    float walkBloomMultiplier = 1.5f;

    [SerializeField, Range(0, 10)]
    float crouchBloomMultiplier = 0.5f;

    [SerializeField, Range(0, 10)]
    float sprintBloomMultiplier = 2f;

    [SerializeField, Range(0, 10)]
    float adsBloomMultiplier = 0.5f;

    MovementManagerState movement;
    AimStateManager aiming;

    float currentBloom;

    void Start()
    {
        movement = GetComponentInParent<MovementManagerState>();
        aiming = GetComponentInParent<AimStateManager>();    
    }

    public Vector3 BloomAngle(Transform barrelPos)
    {
        if (movement.currentState == movement.Idle) 
            currentBloom = defaultBloomAngle;
        else if (movement.currentState == movement.Walk) 
            currentBloom = defaultBloomAngle * walkBloomMultiplier;
        else if (movement.currentState == movement.Run) 
            currentBloom = defaultBloomAngle * sprintBloomMultiplier;
        else if (movement.currentState == movement.Crouch) 
        {
            currentBloom = defaultBloomAngle * crouchBloomMultiplier;
            if (movement.direction.magnitude != 0) 
                currentBloom *= walkBloomMultiplier;
        }

        if (aiming.currentState == aiming.Aim) 
            currentBloom *= adsBloomMultiplier;

        float randX = Random.Range(-currentBloom, currentBloom);
        float randY = Random.Range(-currentBloom, currentBloom);
        float randZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotation = new Vector3(randX, randY, randZ);
        return barrelPos.localEulerAngles + randomRotation; 
    }
}