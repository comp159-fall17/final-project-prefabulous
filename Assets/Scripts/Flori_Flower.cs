using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Flower : MonoBehaviour {

	[Header("VRTK Variables")]
	[Tooltip("Signifies if the flower can be picked")]
	public bool canBePicked = false;
	[Tooltip("Signifies if the flower is attached to a seed")]
	public bool isAttached = true;

	[Header("Flower Data")]
	[Tooltip("Amount of money this flower sells for at the market")]
	public int flowerWorth = 5;


	public void SetCanBePickedTo(bool state)
	{
		canBePicked = state;
	}

	public void Detach()
	{
		isAttached = false;
	}

	public bool CanBePicked()
	{
		return canBePicked;
	}

	public bool IsAttached()
	{
		return isAttached;
	}

}
