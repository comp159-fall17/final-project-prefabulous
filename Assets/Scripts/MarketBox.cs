using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;

public class MarketBox : MonoBehaviour {
	
    [HideInInspector]
    public List<GameObject> inCollision;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (inCollision.Count > 0) 
		{
            VanishItems();
        }

	}

    void VanishItems() 
	{
        List<GameObject> toBeDestroyed = new List<GameObject>();
        foreach (GameObject go in inCollision) 
		{
            Sellable worth = go.GetComponent<Sellable>();
            if (worth != null)
            {
                PiggyBank.Instance.Earn(worth.worth);
                toBeDestroyed.Add(go);
            }
        }
        foreach (GameObject go in toBeDestroyed) 
		{
            inCollision.Remove(go);
            Destroy(go);
        }
    }
}
