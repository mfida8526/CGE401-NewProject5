using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWithRaycasts : MonoBehaviour
{

    public  float damage = 10f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public float hitForce = 10f;
    public Spawner spawner;
    public float timePenalty = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.gameObject.name);

            Target target = hitInfo.transform.gameObject.GetComponent<Target>();
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                // Increase spawn rate (decrease spawn time)
                spawner.IncreaseSpawnRate(); // Adjust value as needed
            }
            else if (hitInfo.transform.CompareTag("Friendly"))
            {
                // Decrease spawn rate (increase spawn time)
                spawner.DecreaseSpawnRate(); // Adjust value as needed
                spawner.DecreaseGameTimer(timePenalty);
            }

            if (target != null)
            {
                target.TakeDamage(damage);

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward) * hitForce, ForceMode.Impulse);
                }
            }
        }
    }
}