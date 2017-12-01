using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour {
    public GameObject box;
    MarketBoxController script;

    private void Start()
    {
        script = box.GetComponent<MarketBoxController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        script.inCollision.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        script.inCollision.Remove(other.gameObject);
    }
}
