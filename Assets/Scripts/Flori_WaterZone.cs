using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_WaterZone : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}

		Debug.Log ("Can in zone");
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}

		Debug.Log ("Out of zone");
	}

}
