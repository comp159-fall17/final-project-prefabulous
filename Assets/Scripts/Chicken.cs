using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour {
	
    public float wanderRadius, wanderTimer, randNumMin, randNumMax;
    public Animator animator;
    public AudioClip scratchSound, clucking;
    Transform target;
    NavMeshAgent agent;
	Vector3 goalPosition, lastPosition;

    float timer, randNum, scratchStart, scratchStop ;
    bool isWalkingTimer = true, scratchDelayGenerated =true, scratchTimerRunning = false, inIdle = true;
    AudioSource audio;
    // Use this for initialization
    void Start () {
		
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        audio = GetComponent<AudioSource>();
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
                audio.PlayOneShot(scratchSound);
            }

            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                wanderTimer = Random.Range(randNumMin, randNumMax);
                timer = 0;
                scratchDelayGenerated = false;
            }
            if (goalPosition == gameObject.transform.position ) //if the chicken is in the target postion before getting a new postion to wander to. 
            {
                if (inIdle)
                {
                    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
                    StartCoroutine(PlayIdle("Armature|Idle_1"));

                }
            }
            if (gameObject.transform.position == lastPosition) // if the chicken runs into the edge of the mesh
            {
                gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            }
            lastPosition = gameObject.transform.position;
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
                audio.PlayOneShot(clucking);
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
    IEnumerator PlayIdle(string animationName)
    {
        inIdle = false;
        gameObject.GetComponent<Animator>().Play(animationName);
        yield return new WaitForSeconds(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1);
        inIdle = true;
    }
}
