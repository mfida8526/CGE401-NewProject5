using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    void Start() { agent = GetComponent<NavMeshAgent>(); }
    void Update() { agent.SetDestination(player.position); }
}
