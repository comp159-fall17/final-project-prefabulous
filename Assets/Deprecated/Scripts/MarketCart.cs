using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketCart : MonoBehaviour {

	public GameObject[] seedPrefabs;
	public Vector3 seedOrigin;
	char[] clone;
	bool sunflower;

	// Use this for initialization
	void Start () {
        clone = new char[] {'(', 'C', 'l', 'o', 'n', 'e', ')' }; // Used to remove that "(Clone)" from the name of the item when it's coin value is looke up.
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Flower"))	{
			FloriPlayerController.instance.ForceDropObject ();
			Debug.Log (other.gameObject.name);
			DJOsvaldo.PlayClipAt ("whoosh", 1f);
			Accountant.AddCoin (other.gameObject.name.TrimEnd(clone));
			Destroy (other.gameObject);

            // Temporary way of generating new seeds w/o full market system.
			GameObject newSeed = (GameObject)Instantiate (GetRandomFlower(), seedOrigin, Quaternion.identity) as GameObject;
			newSeed.name = newSeed.name.TrimEnd (clone);
			newSeed.AddComponent<Rigidbody> ();
		}
	}

	GameObject GetRandomFlower()
	{
		sunflower = !sunflower;
		if (sunflower)
			return seedPrefabs [1];
		else
			return seedPrefabs [0];
	}




}
