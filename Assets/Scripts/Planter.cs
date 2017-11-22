using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour {

	public GameObject flatDirt, raisedDirt;
	public Vector3 seedOrigin = new Vector3(1.5f, 0.25f, 0f);
	public bool hasCrop = false;
	public Seed seedInPlanter;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void PlantCrop(GameObject seed)
	{
		seed.transform.SetParent (transform);
		seed.transform.localPosition = seedOrigin;
		seedInPlanter = seed.GetComponent<Seed>();
		seedInPlanter.collectable = false;
		raisedDirt.SetActive (true);
		seed.SetActive (true);
		seedInPlanter.Sprout ();
		hasCrop = true;
	}

	public void RemoveCropFrom()
	{
		raisedDirt.SetActive (false);
		seedInPlanter = null;
		hasCrop = false;
	}

	public void StartGrowingFlower()
	{
		seedInPlanter.AddWater ();
	}

	public Seed GetSeedInPlanter()
	{
		return seedInPlanter;
	}
}
