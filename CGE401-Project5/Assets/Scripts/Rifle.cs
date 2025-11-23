using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Rifle weapon extension
*/
public class Rifle : MonoBehaviour, IWeapon
{
    public float Damage => 25f;
    public float Range => 25f;  // Shorter distance

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
        Debug.Log("Rifle fired");
    }

    /*   private void Start()
       {
           weaponName = "Rifle";
       }*/
}
