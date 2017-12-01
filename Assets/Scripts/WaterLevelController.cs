using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelController : MonoBehaviour {

	[Tooltip("Y-axis scale of transform when object is in well")]
	[Range(0.11f, 0.16f)]
	public float raisedLevel = 0.15f;

	Vector3 size;

	// Use this for initialization
	void Start() {

		size = transform.localScale;

	}

	void OnTriggerEnter(Collider other)
	{
		size.y = raisedLevel;
		transform.localScale = size;
	}

	void OnTriggerExit(Collider other)
	{
		size.y = 0.1f;
		transform.localScale = size;
	}

}
