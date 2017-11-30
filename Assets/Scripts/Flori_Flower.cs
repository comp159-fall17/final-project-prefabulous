using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Flower : MonoBehaviour {

	[Header("VRTK Variables")]
	[Tooltip("Signifies if the flower can be picked")]
	public bool canBePicked = false;

	[Header("Flower Data")]
	[Tooltip("Amount of money this flower sells for at the market")]
	public int flowerWorth = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
