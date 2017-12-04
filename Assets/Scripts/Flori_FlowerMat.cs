using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_FlowerMat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Flower"))
		{
			return;
		}

		other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Flower"))
		{
			return;
		}

		other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
	}

}
