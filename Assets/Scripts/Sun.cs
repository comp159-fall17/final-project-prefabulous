using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, 1f * Time.deltaTime); //makes sun rotate around 0 point right axis for rotation
        transform.LookAt(Vector3.zero);
    }
}
