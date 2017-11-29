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
	public bool canInZone = false;

	Flori_Seed seedInPlanter;

	void Update()
	{
		Debug.Log ("Is being watered: " + IsBeingWatered());
	}

	protected override void SnapObjectToZone (VRTK_InteractableObject objectToSnap)
	{
		base.SnapObjectToZone (objectToSnap);
		PlantSeed (objectToSnap);
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
			seedInPlanter.collectable = false;
			LockSeedInPlanter();

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
        seedInPlanter.AddWater();
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
	/// Sets the can in zone bool used to add water to seed.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void SetCanInZone(bool state)
	{
		canInZone = state;
	}


	bool IsBeingWatered()
	{
		return canInZone;
	}

}
