using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Maile Fidale
    * Project 5
    * Shotgun weapon extension
*/
public class Shotgun : MonoBehaviour, IWeapon
{
    [Header("Shotgun Settings")]
    public int pellets = 8;              // Number of pellets per shot
    public float spreadAngle = 15f;      // Spread angle in degrees
    public float damage = 8f;            // Damage per pellet
    public float range = 20f;            // Short-range

    private PlayerShooting shooter;

    void Awake()
    {
        shooter = GetComponentInParent<PlayerShooting>();
        if (shooter == null)
            Debug.LogError("ShotgunWeapon: PlayerShooting not found in parent!");
    }

    public float Damage => damage;
    public float Range => range;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            // Calculate random spread
            Vector3 direction = shooter.transform.forward;
            direction = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            ) * direction;

            // Fire the pellet
            RaycastHit hit;
            if (Physics.Raycast(shooter.transform.position, direction, out hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);

                    // Notify GameManager
                    if (target.isInfected)
                        shooter.GetComponent<GameManager>()?.HitInfectedAnimal();
                    else
                        shooter.GetComponent<GameManager>()?.HitNormalAnimal();
                }
            }
        }

        Debug.Log("Shotgun fired!");
    }
    /*   private void Start()
       {
           weaponName = "Shotgun";
       }*/
}
