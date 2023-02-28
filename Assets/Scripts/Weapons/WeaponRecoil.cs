using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Transform recoilFollowPos;
    
    [SerializeField] float kickBackAmount = -1;
    
    [SerializeField] float kickBackSpeed = 10, returnSpeed = 20;
    float currentRecoilPosition;

    private void Update()
    {
        currentRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, returnSpeed * Time.deltaTime);
        float finalRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, kickBackSpeed * Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPosition);
    }

    public void TriggerRecoil() => currentRecoilPosition += kickBackAmount;
}