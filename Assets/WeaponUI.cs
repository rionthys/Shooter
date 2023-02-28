using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] 
    private Text ammo;
    [SerializeField] 
    private WeaponAmmo weaponAmmo;

    void Update()
    {
        ammo.text = weaponAmmo.currentAmmo + "/" + weaponAmmo.extraAmmo;
    }
}
