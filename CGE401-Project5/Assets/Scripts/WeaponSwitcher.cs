using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Allows player to switch weapons
*/
public class WeaponSwitcher : MonoBehaviour
{
    public Weapons[] weapons;
    private IWeapon currentWeapon;
    private int currentIndex = 0;

    void Start()
    {
        SelectWeapon(0);
    }

    void Update()
    {
        // Switch weapons with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
    }

    void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        // Deactivate all weapons
        foreach (var w in weapons)
            w.Deactivate();

        // Activate chosen one
        currentWeapon = weapons[index].GetComponent<IWeapon>();
        currentWeapon.Activate();

        currentIndex = index;
        Debug.Log($"Switched to {weapons[index].WeaponName}");
    }
}
