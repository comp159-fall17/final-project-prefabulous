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

		GetComponentInChildren<Flori_Planter> ().SetCanInZone (true);
		Debug.Log ("Can in zone");
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}

		GetComponentInChildren<Flori_Planter> ().SetCanInZone (false);
		Debug.Log ("Out of zone");
	}

}
