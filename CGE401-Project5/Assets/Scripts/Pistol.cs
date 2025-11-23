using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Pistol weapon extension
*/
public class Pistol : MonoBehaviour, IWeapon
{
    public float Damage => 10f;
    public float Range => 100f;

    private PlayerShooting shooter;

    void Awake()
    {
        shooter = GetComponentInParent<PlayerShooting>();
    }

    public void Activate() { gameObject.SetActive(true); }
    public void Deactivate() { gameObject.SetActive(false); }

    public void Shoot()
    {
        shooter.FireRaycast(Damage, Range);
        Debug.Log("Pistol fired");
    }
    /*    private void Start()
        {
            weaponName = "Pistol";
        }*/
}
