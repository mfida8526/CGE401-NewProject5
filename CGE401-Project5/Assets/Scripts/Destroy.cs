using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Mimi Davis
* Destroy
* Project5
* Destroys animals when they hit the player
*/
public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy this game object (the one this script is attached to)
            Destroy(gameObject);
        }
    }
}
