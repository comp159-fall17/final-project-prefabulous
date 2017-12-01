using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Well : MonoBehaviour {

	[Header("Fill Speed")]
	[Tooltip("Period in seconds in which the fill amount (below) is added to the watering can")]
	[Range(0.05f, 2.0f)]
	public float fillPeriod = 0.1f;
	[Tooltip("Amount the well replenishes to the watering can each fill period.")]
	[Range(1, 100)]
	public int fillAmount = 1;

	Flori_WateringCan fwc;
	bool isFilling = false;
	float fillCounter = 0f;

	// Update is called once per frame
	void Update () {

		if (isFilling && fwc != null && !fwc.IsFull())
		{
			fillCounter += Time.deltaTime;
			if (fillCounter >= fillPeriod)
			{
				fwc.AddWaterOf (fillAmount);
				fillCounter = 0f;
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if (!other.CompareTag("Can Body"))
		{
			return;
		}

		fwc = other.GetComponent<Flori_WateringCan> ();
		fwc.SetIsSubmerged (true);
		isFilling = true;

	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Can Body"))
		{
			return;
		}

		fwc.SetIsSubmerged (false);
		fwc = null;
		isFilling = false;

	}

	void AddWaterToCan()
	{

	}

}
