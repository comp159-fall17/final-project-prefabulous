using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour {
	
    public GameObject box;
    MarketBox script;

    private void Start()
    {
        script = box.GetComponent<MarketBox>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Sellable>() != null)
		{
            script.inCollision.Add(other.gameObject);
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Sellable>() != null)
		{
            script.inCollision.Remove(other.gameObject);
		}
    }
}
