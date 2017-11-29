using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class Flori_Planter : VRTK_SnapDropZone {
    public bool hasCrop = false;
    Flori_Seed seedInPlanter;
    // Use this for initialization
    void Start () {
		
	}

	protected override void SnapObjectToZone (VRTK_InteractableObject objectToSnap)
	{
		base.SnapObjectToZone (objectToSnap);
		PlantSeed (objectToSnap);
	}

	void PlantSeed(VRTK_InteractableObject seed)
	{
		try 
		{
            seedInPlanter = seed.GetComponent<Flori_Seed>();
            seedInPlanter.collectable = false;
            // seed.SetActive(true);
            //should I use the line below 
            //seedInPlanter.SetActive(true);
            seedInPlanter.Sprout();
            hasCrop = true;
        }
		catch (NullReferenceException ex)
        {
			Debug.Log ("Tried to snap a not-seed in this seed orb. No not-seeds allowed!");
			// TODO: decide if we really need to check once we get the Policy List working for these zones
		}
	}

    public void RemoveCropFrom()
    {
        seedInPlanter = null;
        hasCrop = false;
    }

    public void StartGrowingFlower()
    {
        seedInPlanter.AddWater();
    }

    public Flori_Seed GetSeedInPlanter()
    {
        return seedInPlanter;
    }
}
