using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    * Maile Fidale
    * Project 5
    * Extension
*/
public class Animal : MonoBehaviour
{
    [HideInInspector]
    public Transform player;
    private NavMeshAgent agent;

    protected virtual void Start()
    {
        // Find player in the scene
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your player has the 'Player' tag.");
        }

        // Get NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on " + gameObject.name);
        }
    }

    protected virtual void Update()
    {
        // Chase the player
        if (player != null && agent != null)
        {
            agent.SetDestination(player.position);
        }
    }

    /*[HideInInspector]
    public Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool isInfected;
    public float moveSpeed = 3f;
    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }*/
}
