using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    public ReloadState reloadState = new ReloadState();
    public DefaultState defaultState = new DefaultState();
    public SwapState swapState = new SwapState();

    [HideInInspector] public WeaponManger currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
    AudioSource audioSource; 

    [HideInInspector] public Animator anim;

    [SerializeField] public MultiAimConstraint rHandAim;
    [SerializeField] public TwoBoneIKConstraint lHandIK;


    void Start()
    {
        SwitchState(defaultState);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ReloadWeapon()
    {
        ammo.Reload();
        SwitchState(defaultState);
    }

    public void MugOut()
    {
        audioSource.PlayOneShot(ammo.magOutSound);
    }

    public void MagIn()
    {
        audioSource.PlayOneShot(ammo.magInSound);
    }

    public void ReleaseSlide()
    {
        audioSource.PlayOneShot(ammo.releaseSlideSound);
    }

    public void SetWeapon(WeaponManger weapon){
        currentWeapon = weapon;
        audioSource = weapon.audioSource;
        ammo = weapon.ammo;
    }
}