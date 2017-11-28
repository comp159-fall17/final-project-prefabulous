using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterInVr : MonoBehaviour {
    public Seed seedInPlanter; //is there a way to get the seed dynamically
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
