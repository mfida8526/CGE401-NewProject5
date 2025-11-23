using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public IWeapon currentWeapon;  // Assigned by WeaponSwitcher
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon != null)
                currentWeapon.Shoot();
        }
    }

    // Shared raycast logic called by weapons
    public void FireRaycast(float damage, float range)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);

                if (target.isInfected)
                    gameManager.HitInfectedAnimal();
                else
                    gameManager.HitNormalAnimal();
            }
        }
    }
    /*    public float damage = 10f;
        public float range = 100f;
        // Reference to the GameManager to update state
        private GameManager gameManager;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene!");
            }
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1")) // Assuming "Fire1" is left mouse button
            {
                Shoot();
            }
        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                Debug.Log("Hit: " + hit.transform.name);

                // Try to get the Animal script from the hit object
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage); // Optional: if animals have health
                    if (target.isInfected)
                    {
                        gameManager.HitInfectedAnimal();
                    }
                    else
                    {
                        gameManager.HitNormalAnimal();
                    }
                }
            }
        }*/
}

