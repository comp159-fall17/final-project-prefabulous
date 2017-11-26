using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabbedObjectRelayer : VRTK_InteractGrab {

	GameObject grabbedObject;

	// Use this for initialization
	void Start () {

		grabbedObject = GetGrabbedObject();

	}
	
	// Update is called once per frame
	void Update () {

		if (GetGrabbedObject() != null) Debug.Log (GetGrabbedObject().name);

	}
}
