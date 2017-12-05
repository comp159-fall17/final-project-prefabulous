using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;

public class MarketBox : MonoBehaviour {
    public GameObject lidObject;
    [HideInInspector]
    public List<GameObject> inCollision;

    VRTK_PhysicsRotator lid;

	// Use this for initialization
	void Start () {
        lid = lidObject.GetComponent<VRTK_PhysicsRotator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!(lid.GetNormalizedValue() > Mathf.Epsilon)) {
            if (inCollision.Count > 0) {
                VanishItems();
            }
        }
	}

    void VanishItems() {
        Debug.Log("Vanishing items.");
        List<GameObject> toBeDestroyed = new List<GameObject>();
        foreach (GameObject go in inCollision) {
            Sellable worth = go.GetComponent<Sellable>();
            if (worth != null)
            {
                PiggyBank.Instance.Earn(worth.worth);
                toBeDestroyed.Add(go);
            }
        }
        foreach (GameObject go in toBeDestroyed) {
            inCollision.Remove(go);
            Destroy(go);
        }
    }
}
