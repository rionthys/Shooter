using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;

    ActionStateManager actions;
    public WeaponManger[] weapons;
    public WeaponUI[] weaponsUI;
    int currentWeaponIndex;

    private void Awake()
    {
        actions = GetComponent<ActionStateManager>();
        currentWeaponIndex = 0;
        weapons = GetComponentsInChildren<WeaponManger>();
        weaponsUI = GetComponentsInChildren<WeaponUI>();

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0) weapons[0].gameObject.SetActive(true);
            else weapons[i].gameObject.SetActive(false);
            
            if (i == 0) weaponsUI[0].gameObject.SetActive(true);
            else weaponsUI[i].gameObject.SetActive(false);
        }
    }

    public void SetCurrentWeapon(WeaponManger weapon)
    {
        if (actions == null) actions = GetComponent<ActionStateManager>();
        leftHandIK.data.target = weapon.leftHandTarget;
        leftHandIK.data.hint = weapon.leftHandHint;
        actions.SetWeapon(weapon);
    }

    public void ChangeWeapon(float direction)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weaponsUI[currentWeaponIndex].gameObject.SetActive(false);
        if (direction < 0)
        {
            if (currentWeaponIndex == 0) currentWeaponIndex = weapons.Length - 1;
            else currentWeaponIndex--;
        }
        else
        {
            if (currentWeaponIndex == weapons.Length - 1) currentWeaponIndex = 0;
            else currentWeaponIndex++;
        }
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        weaponsUI[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actions.defaultState.scrollDirection);
    }

    public void WeaponPulledOut()
    {
        actions.SwitchState(actions.defaultState);
    }

}