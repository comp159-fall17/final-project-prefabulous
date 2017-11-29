using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Seed : MonoBehaviour {
    public bool collectable = true, isGrowing = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Not sure if need to add in the rest of the code
    public void Sprout()
    {
        Destroy(this.GetComponent<Rigidbody>());
    }
    public void AddWater()
    {
        isGrowing = true;
    }
}
