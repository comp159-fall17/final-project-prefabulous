using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;

public class PiggyBankController : VRTK_ChildOfControllerGrabAttach {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint) {
        bool result = base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
        GetComponent<AudioSource>().Play();
        return result;
    }
}
