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
    public MonoBehaviour[] weapons;   // These MUST have scripts implementing IWeapon
    private IWeapon currentWeapon;
    private int currentIndex = 0;

    private PlayerShooting playerShooting;

    void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();

        if (playerShooting == null)
            Debug.LogError("WeaponSwitcher requires PlayerShooting on the same GameObject");

        SelectWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
    }

    void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        // Turn OFF all weapons
        foreach (var w in weapons)
        {
            IWeapon weapon = w as IWeapon;
            if (weapon != null) weapon.Deactivate();
        }

        // Set the new current weapon
        currentWeapon = weapons[index] as IWeapon;

        if (currentWeapon == null)
        {
            Debug.LogError($"Weapon at index {index} does NOT implement IWeapon!");
            return;
        }

        currentWeapon.Activate();
        playerShooting.currentWeapon = currentWeapon;

        currentIndex = index;

        Debug.Log("Switched to: " + weapons[index].name);
    }
}
