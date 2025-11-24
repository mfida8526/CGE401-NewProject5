using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    void Awake() => agent = GetComponent<NavMeshAgent>();

    void Update()
    {
        if (player != null && Vector3.Distance(agent.destination, player.position) > 0.1f)
            agent.SetDestination(player.position);
    }
}