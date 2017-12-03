using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour {
	
    public float wanderRadius;
    public float wanderTimer;
    public Animator animator;

    Transform target;
    NavMeshAgent agent;
	Vector3 goalPosition;

    float timer, randNum, scratchStart, scratchStop ;
    bool isWalkingTimer = true, scratchDelayGenerated =true, scratchTimerRunning = false;

    // Use this for initialization
    void Start () {
		
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

    }

    // Update is called once per frame
    void Update()
    {
        if (isWalkingTimer)
        {
            if (!scratchDelayGenerated)
            {
                scratchStart = 1; // left these two float values like this because of the animation clip time
                scratchStop = 2f;
                scratchDelayGenerated = true;
                scratchTimerRunning = true;
                isWalkingTimer = false;
                agent.SetDestination(gameObject.transform.position);
            }

            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                timer = 0;
                scratchDelayGenerated = false;
            }
            if (goalPosition == gameObject.transform.position) //if the chicken is in the target postion before getting a new postion to wander to
            {            
               gameObject.GetComponent<Animator>().Play("Armature|Idle_1");          
            }
            if (!gameObject.GetComponent<Rigidbody>().IsSleeping()) //moving
            {
                gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            }
           
        }
        else if (scratchTimerRunning)
        {
            scratchStart += Time.deltaTime;
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            gameObject.GetComponent<Animator>().Play("Armature|Eating");
            if (scratchStart >= scratchStop)
            {
                scratchTimerRunning = false;
                isWalkingTimer = true;
                scratchDelayGenerated = true;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                gameObject.GetComponent<Animator>().SetBool("isWalking", true);
                goalPosition = newPos;
            }
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
