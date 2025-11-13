using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject[] prefabsToSpawn;

    private float leftBound = -5;
    private float rightBound = 4;
    private float spawnPosZ = 20;


   
    void Start()
    {
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        yield return new WaitForSeconds(3f);
    }
    // Update is called once per frame
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
