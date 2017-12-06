using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResetter : MonoBehaviour
{

    [Tooltip("Time after which this object will reset from touching the ground.")]
    public float resetTime = 30f;

    Vector3 startingPosition;
    Vector3 startingRotation;
    float resetCounter = 0f;

    // Use this for initialization
    void Start()
    {

        startingPosition = transform.position;
        startingRotation = transform.eulerAngles;

    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            return;
        }

        resetCounter += Time.deltaTime;
        if (resetCounter >= resetTime)
        {
            transform.position = startingPosition;
            transform.eulerAngles = startingRotation;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            return;
        }

        resetCounter = 0f;
    }

}