using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour {

	public GameObject worldCan, camCan;
	public static GameObject WorldCan, CamCan;
	public static bool PlayerIsHolding = false;

	// Use this for initialization
	void Start () {
		WorldCan = worldCan;
		CamCan = camCan;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void EquipWateringCan()
	{
		WorldCan.SetActive (false);
		CamCan.SetActive (true);
		PlayerIsHolding = true;
	}



}
