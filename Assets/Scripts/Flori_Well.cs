using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Well : MonoBehaviour {
    public GameObject wateringCan;
    public int waterVolume;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Refill Point"))
        {
            gameObject.GetComponent<Transform>().position += Vector3.up * Time.deltaTime;
            wateringCan.GetComponent<Flori_WateringCan>().AddWaterOf(waterVolume);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Refill Point"))
        {
            gameObject.GetComponent<Transform>().position += Vector3.down * Time.deltaTime;
        }
    }
}
