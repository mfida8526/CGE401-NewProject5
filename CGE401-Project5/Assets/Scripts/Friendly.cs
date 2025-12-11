using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    * Maile Fidale
    * Project 5
    * Uninfected animal movement/ roaming
*/
public class Friendly : MonoBehaviour
{
    public float stopDistance = 0.5f;
    public float minOffset = 2f;
    public float maxOffset = 4f;

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 offsetTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Generate a random offset around the player
        float radius = Random.Range(minOffset, maxOffset);
        float angle = Random.Range(0f, Mathf.PI * 2f);

        offsetTarget = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }

    void Update()
    {
        if (player == null) return;

        // The target position with the offset applied
        Vector3 targetPos = player.position + offsetTarget;

        // Tell the NavMeshAgent to move there
        agent.SetDestination(targetPos);

        // If close enough → disappear
        if (!agent.pathPending && agent.remainingDistance <= stopDistance)
        {
            Destroy(gameObject);
        }
    }
}
