using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

	public float maxSeeds, stageOneHeightLimit = 350f, stageTwoHeightLimit = 350f;
	public GameObject seedPrefab;
	public bool collectable = true, isGrowing = false, stageOne = true, stageTwo = false;

	float diameter, floweringSpeed = 1.0f;
	public GameObject flower;
	Vector3 flowerScale, flowerPosition;

	// Use this for initialization
	void Start () {

		diameter = transform.localScale.x;
		for (int i = 0; i< maxSeeds; i++) {
			GenerateSeed ();
		}

	}

	void GenerateSeed()
	{
		Vector3 seedPosition = transform.position;
		seedPosition.x += RandomOffset (diameter * 4);
		seedPosition.z += RandomOffset (diameter * 4);

		if (Vector3.Distance(transform.position, seedPosition) < diameter) {
			GenerateSeed ();
		}

		GameObject seedling = Instantiate (seedPrefab, seedPosition, Camera.main.transform.rotation);
		seedling.name = gameObject.name;
		seedling.transform.SetParent (transform);
	}

	float RandomOffset(float offsetSize)
	{
		return Random.Range (-offsetSize/2.0f, offsetSize/2.0f);
	}

	public void Sprout()
	{
		Destroy (this.GetComponent<Rigidbody> ());
		string flowerName = gameObject.name.Split (' ')[0];
		if (FlowerDictionary.FlowerIsInHandbook (flowerName)) {
			flower = FlowerDictionary.GetFlowerModelFromName (flowerName);
			flower.transform.SetParent (transform);
			flower.transform.localPosition = Vector3.zero;
			floweringSpeed = FlowerDictionary.GetFloweringSpeedFromName (flowerName);
		}
	}

	public void AddWater()
	{
		isGrowing = true;
	}

	public void FinishGrowing()
	{
		stageTwo = false;
		isGrowing = false;
		flower.GetComponent<Flower>().SetCanBePicked(true);
	}

	void StageOneGrowth()
	{
		flowerScale = flower.transform.localScale;
		flowerScale.y += Time.deltaTime * floweringSpeed;
		if (flowerScale.x < 200f) {
			flowerScale.x += Time.deltaTime * floweringSpeed / 3.0f;
			flowerScale.z += Time.deltaTime * floweringSpeed / 3.0f;
		}
		flower.transform.localScale = flowerScale;

		if (flowerScale.y >= stageOneHeightLimit) {
			stageOne = false;
			stageTwo = true;
			isGrowing = false;
		}
	}

	void StageTwoGrowth() 
	{
		flowerScale = flower.transform.localScale;
		flowerScale.y += Time.deltaTime * floweringSpeed;
		flowerScale.x += Time.deltaTime * floweringSpeed / 5.0f;
		flowerScale.z += Time.deltaTime * floweringSpeed / 5.0f;
		flower.transform.localScale = flowerScale;

		if (flowerScale.y >= stageTwoHeightLimit) {
			FinishGrowing ();
		}
	}

	// Update is called once per frame
	void Update () {

		if (isGrowing) {
			if (stageOne)
				StageOneGrowth ();
			else if (stageTwo)
				StageTwoGrowth ();
		}
	}



}