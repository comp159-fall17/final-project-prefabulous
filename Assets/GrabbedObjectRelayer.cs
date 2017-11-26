using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabbedObjectRelayer : VRTK_InteractGrab {

	// Use this for initialization
	void Start () {

		grabbedObject = GetGrabbedObject();

	}

	protected override void PerformGrabAttempt (GameObject objectToGrab)
	{
		base.PerformGrabAttempt (objectToGrab);
		if (objectToGrab != null) {
			Debug.Log (objectToGrab.name);
		}
	}

}
