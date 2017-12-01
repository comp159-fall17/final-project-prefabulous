using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;

public class MarketBoxController : MonoBehaviour {
    public GameObject lidObject;
    public GameObject colliderObject;

    VRTK_PhysicsRotator lid;
    Collider collider;

	// Use this for initialization
	void Start () {
        lid = lidObject.GetComponent<VRTK_PhysicsRotator>();
        collider = colliderObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void VanishItems() {
        //GameObject[] inCollision = collider.
    }
}
