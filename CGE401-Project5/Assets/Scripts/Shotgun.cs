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

    // Properties required by IWeapon
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
        // Track targets hit this shot to notify GameManager only once per target
        HashSet<Target> hitTargets = new HashSet<Target>();

        for (int i = 0; i < pellets; i++)
        {
            // Calculate random spread for each pellet
            Vector3 direction = shooter.transform.forward;
            direction = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            ) * direction;

            // Fire the pellet
            if (Physics.Raycast(shooter.transform.position, direction, out RaycastHit hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    // Apply damage per pellet
                    target.TakeDamage(damage);

                    // Notify GameManager only once per target
                    if (!hitTargets.Contains(target))
                    {
                        hitTargets.Add(target);
                        GameManager gm = shooter.GetComponent<GameManager>();
                        if (gm != null)
                        {
                            if (target.isInfected)
                                gm.HitInfectedAnimal();
                            else
                                gm.HitNormalAnimal();
                        }
                    }
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
