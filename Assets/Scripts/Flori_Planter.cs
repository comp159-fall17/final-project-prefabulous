using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class Flori_Planter : VRTK_SnapDropZone {
    
	[Header("Watering Can Properties")]
	[Tooltip("Signifies if the planter has a crop planted in it")]
	public bool hasCrop = false;
	[Tooltip("True if watering can collider is within bounds to plant")]
	public bool canIsInRange = false;

	Flori_Seed seedInPlanter;
	float waterCounter = 0f;
	int waterDropsToBloom;
	int waterDropsReceived = 0;

	protected override void SnapObjectToZone (VRTK_InteractableObject objectToSnap)
	{
		base.SnapObjectToZone (objectToSnap);
		PlantSeed (objectToSnap);
	}

	void Update() {

		if (seedInPlanter != null && IsBeingWatered() && !seedInPlanter.IsGrowing())
		{
			UpdateWaterInPlanter ();
		}

	}

	/// <summary>
	/// Plants the seed in this Planter instance.
	/// </summary>
	/// <param name="seed">Seed.</param>
	void PlantSeed(VRTK_InteractableObject seed)
	{
		try 
		{
            seedInPlanter = seed.GetComponent<Flori_Seed>();
			LockSeedInPlanter();
			waterDropsToBloom = seedInPlanter.GetWaterDropsToBloom();

			hasCrop = true;
            seedInPlanter.Sprout();
        }
		catch (NullReferenceException ex)
        {
			Debug.Log ("Tried to snap a not-seed in this seed orb. No not-seeds allowed!");
			// TODO: decide if we really need to check once we get the Policy List working for these zones
		}
	}

	/// <summary>
	/// Removes the crop from this planter.
	/// </summary>
    public void RemoveCropFrom()
    {
        seedInPlanter = null;
        hasCrop = false;
    }

	/// <summary>
	/// Starts growing the flower inside this planter.
	/// </summary>
    public void GrowFlower()
    {
        seedInPlanter.StartGrowing();
    }

	/// <summary>
	/// Gets the seed in planter.
	/// </summary>
	/// <returns>The seed in planter.</returns>
    public Flori_Seed GetSeedInPlanter()
    {
        return seedInPlanter;
    }

	/// <summary>
	/// Prevents grabbing the seed from this planter.
	/// </summary>
	void LockSeedInPlanter()
	{
		if (seedInPlanter != null)
		{
			seedInPlanter.GetComponent<VRTK_InteractableObject> ().isGrabbable = false;
		}
	}

	/// <summary>
	/// Sets the canIsInRange bool used to add water to seed.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void SetCanInRange(bool state)
	{
		canIsInRange = state;
	}

	/// <summary>
	/// Determines whether this instance is being watered.
	/// </summary>
	/// <returns><c>true</c> if this instance is being watered; otherwise, <c>false</c>.</returns>
	bool IsBeingWatered()
	{
		return canIsInRange && Flori_WateringCan.Instance.CanIsPouring();
	}

	void UpdateWaterInPlanter()
	{
		waterCounter += Time.deltaTime;
		if (waterCounter >= Flori_WateringCan.Instance.pouringInterval)
		{
			Debug.Log ("Tick");
			waterDropsReceived++;

			if (waterDropsReceived == waterDropsToBloom)
			{
				waterDropsReceived = 0;
				seedInPlanter.StartGrowing ();
			}

			waterCounter = 0f;
		}
	}

}
