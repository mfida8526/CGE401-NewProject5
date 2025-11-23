using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Weapon interface
*/
public interface IWeapon
{
    void Activate();
    void Deactivate();

    float Damage { get; }
    float Range { get; }

    void Shoot();          // Weapon-specific firing logic
}
