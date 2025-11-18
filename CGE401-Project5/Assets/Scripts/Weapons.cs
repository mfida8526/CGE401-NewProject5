using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Reads other weapon scripts
*/
public abstract class Weapons : MonoBehaviour, IWeapon
{
    [SerializeField] protected string weaponName;

    // Public getter so other scripts can read the name
    public string WeaponName => weaponName;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        Debug.Log(weaponName + " activated");
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        Debug.Log(weaponName + " deactivated");
    }
}
