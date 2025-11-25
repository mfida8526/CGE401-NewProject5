using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Slider


    public class PlayerSpawner : MonoBehaviour
    {
        private GameManager gameManager;
        public Slider slider;
        public Slider slider2; 
        [SerializeField] private GameObject playerPrefab; // Assign your player prefab in the Inspector
        [SerializeField] private Transform winSpawnPoint; // Assign your "WinSpawnPoint" GameObject's Transform here

        public void SpawnPlayerAtWinPoint()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player Prefab is not assigned in PlayerSpawner!");
                return;
            }

            if (winSpawnPoint == null)
            {
                Debug.LogError("Win Spawn Point is not assigned in PlayerSpawner!");
                return;
            }

            // Find existing player and destroy it (optional, depending on your game)
            GameObject existingPlayer = GameObject.FindWithTag("Player"); 
            if (existingPlayer != null)
            {
                Destroy(existingPlayer);
            }

            // Instantiate a new player at the win spawn point's position and rotation
            Instantiate(playerPrefab, winSpawnPoint.position, winSpawnPoint.rotation);
            Debug.Log("Player spawned at win point!");
            gameManager.GameWon();
            slider.gameObject.SetActive(false);
            slider2.gameObject.SetActive(true);
        }
    }
