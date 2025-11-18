using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam; // Assign your main camera here in the Inspector

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Default Unity Input for left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Check if the hit object is a Civilian
            Friendly friendly = hit.transform.GetComponent<Friendly>();
            if (friendly != null)
            {
                GameManager.Instance.HitFriendly();
                return; // Stop further processing if it's a civilian
            }

            // Check if the hit object is a Zombie/Enemy
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}

