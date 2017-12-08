using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_WaterZone : MonoBehaviour {
    public GameObject dirt;
    public Material wetDirt;
    public float wetTime = 5.0f;

    Material dryDirt;
    float timeOfLastWatering;

    private void Start()
    {
        dryDirt = dirt.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        DryUpDirt();
    }

    void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}
		GetComponentInChildren<Flori_Planter> ().SetCanInRange (true);
        if (Flori_WateringCan.Instance.CanIsPouring()) {
            dirt.GetComponent<MeshRenderer>().material = wetDirt;
            timeOfLastWatering = Time.time;
        }
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Watering Can"))
		{
			return;
		}
		GetComponentInChildren<Flori_Planter> ().SetCanInRange (false);
	}

    void DryUpDirt() 
	{
        if (TimeElapsed() > wetTime) {
            dirt.GetComponent<MeshRenderer>().material = dryDirt;
        }
    }

    float TimeElapsed() {
        return Time.time - timeOfLastWatering;
    }
}
