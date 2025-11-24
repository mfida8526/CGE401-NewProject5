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
    public int pellets = 8;
    public float spreadAngle = 15f;
    public float damage = 8f;
    public float range = 20f;

    private PlayerShooting shooter;
    private GameManager gameManager;

    void Awake()
    {
        shooter = GetComponentInParent<PlayerShooting>();
        if (shooter == null)
            Debug.LogError("ShotgunWeapon: PlayerShooting not found in parent!");

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
            Debug.LogError("GameManager not found in scene!");
    }

    public float Damage => damage;
    public float Range => range;

    public void Activate() => gameObject.SetActive(true);
    public void Deactivate() => gameObject.SetActive(false);

    public void Shoot()
    {
        HashSet<Target> hitTargets = new HashSet<Target>();

        for (int i = 0; i < pellets; i++)
        {
            // Cast from camera, NOT shotgun object
            Vector3 origin = Camera.main.transform.position;
            Vector3 direction = Camera.main.transform.forward;

            direction = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            ) * direction;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);

                    if (!hitTargets.Contains(target))
                    {
                        hitTargets.Add(target);
                        gameManager.HitInfectedAnimal();   // All animals are infected now
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
