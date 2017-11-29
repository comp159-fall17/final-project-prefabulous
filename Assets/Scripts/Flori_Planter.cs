using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Flori_Planter : VRTK_SnapDropZone {
    
	public Seed seedInPlanter; //is there a way to get the seed dynamically
    // Use this for initialization
    void Start () {
		
	}

	protected override void SnapObjectToZone (VRTK_InteractableObject objectToSnap)
	{
		base.SnapObjectToZone (objectToSnap);
		PlantSeed (objectToSnap);
	}

	void PlantSeed(GameObject seed)
	{
		try 
		{
			seedInPlanter = seed.GetComponent<Seed> ();	
		}
		catch
		{
			Debug.Log ("Tried to snap a not-seed in this seed orb. No not-seeds allowed!");
			// TODO: decide if we really need to check once we get the Policy List working for these zones
		}
	}

    public void RemoveCropFrom()
    {
        seedInPlanter = null;
    }

    public void StartGrowingFlower()
    {
        seedInPlanter.AddWater();
    }

    public Seed GetSeedInPlanter()
    {
        return seedInPlanter;
    }
}
