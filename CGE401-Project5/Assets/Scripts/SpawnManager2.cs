using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2 : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public GameObject[] friendlyPrefabs;
    public float spawnRadius = 10f;
    public float friendlySpawnRate = 5f;

    private float nextZombieSpawnTime;
    private float nextFriendlySpawnTime;

    void Start()
    {
        nextZombieSpawnTime = Time.time + GameManager.Instance.zombieSpawnRate;
        nextFriendlySpawnTime = Time.time + friendlySpawnRate;
    }

    void Update()
    {
        // Update zombie spawn rate based on GameManager
        if (Time.time >= nextZombieSpawnTime)
        {
            SpawnObject(zombiePrefabs);
            nextZombieSpawnTime = Time.time + GameManager.Instance.zombieSpawnRate;
        }

        if (Time.time >= nextFriendlySpawnTime)
        {
            SpawnObject(friendlyPrefabs);
            nextFriendlySpawnTime = Time.time + friendlySpawnRate;
        }
    }

    void SpawnObject(GameObject[] prefabs)
    {
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        // Ensure the object spawns at the correct y-level (adjust as needed for 3D terrain)
        randomPosition.y = transform.position.y; 

        Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
    }
}

