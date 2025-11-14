using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject[] prefabsToSpawn;

    private float leftBound = -5;
    private float rightBound = 5;
    private float spawnPosZ = 20;


    public HealthSystem healthSystem;
    void Start()
    {

        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();

        StartCoroutine(SpawnRandomPrefabWithCoroutine());

    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        yield return new WaitForSeconds(2f);

        while (!healthSystem.gameOver)
        {
            SpawnRandomPrefab();

            float randomDelay = Random.Range(1.5f, 2.0f);
            yield return new WaitForSeconds(randomDelay);
        }
    }
    void Update()
    {
    }

    void SpawnRandomPrefab()
    {
         int prefabIndex = Random.Range(0, prefabsToSpawn.Length); 
            
            Vector3 spawnPos = new Vector3(Random.Range(leftBound, rightBound), 0, spawnPosZ);

            Instantiate(prefabsToSpawn[prefabIndex], spawnPos, prefabsToSpawn[prefabIndex].transform.rotation);
    }
}
