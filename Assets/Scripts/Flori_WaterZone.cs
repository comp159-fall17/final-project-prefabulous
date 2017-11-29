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
		GetComponentInChildren<Flori_Planter> ().SetCanInRange (true);
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}
		GetComponentInChildren<Flori_Planter> ().SetCanInRange (false);
	}

}
