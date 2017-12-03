using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour {
    public float wanderRadius;
    public float wanderTimer;
    public Animator animator;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private bool isWalking = false;
    private float transitionDuration = 100f;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if (!gameObject.GetComponent<Rigidbody>().IsSleeping())
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
