using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Flori_Seed : MonoBehaviour {

	[Header("Growth Variables")]
	[Tooltip("Signifies if flower is currently growing")]
	public bool isGrowing = false;
	[Tooltip("Height limit for the first stage of flower growth")]
	public float stageOneHeightLimit = 350f;
	[Tooltip("Height limit for the second stage of flower growth")]
	public float stageTwoHeightLimit = 600f;
	[Tooltip("Reduced rate multiplier for stage two growth along the x and z axes")]
	[Range(0.05f, 1f)]
	public float stageTwoReducer = 0.8f;
	[Tooltip("Amount of water drops needed to trigger growth")]
	[Range(1, 100)]
	public int waterDropsToBloom = 5;

	[Header("Flower Data")]
	[Tooltip("The flower prefab the seed will sprout once watered")]
	public GameObject flowerModel;
	[Tooltip("Rate at which this flower grows")]
	[Range(0.1f, 2f)]
	public float growthRate = 0.5f;

	[Header("Testing Variables")]
	[Tooltip("Flower is currently set to grow until its first height limit")]
	public bool inStageOneGrowth = true;
	[Tooltip("Flower has grown to its first height limit and is set to grow until its second and final height limit")]
	public bool inStageTwoGrowth = false;

	GameObject flower;
	Vector3 flowerScale;

	// Update is called once per frame
	void Update () {

		if (isGrowing)
		{
			if (inStageOneGrowth) 
			{
				StageOneGrowth ();
			}
			else if (inStageTwoGrowth)
			{
				StageTwoGrowth();
			}
		}
	}

	//Not sure if need to add in the rest of the code
	public void Sprout()
	{
		flower = (GameObject)Instantiate (flowerModel, transform);
		flower.name = flower.name.TrimEnd (new char[] {'(', 'C', 'l', 'o', 'n', 'e', ')' });
		SetFlowerComponents (false);
		flower.transform.localPosition = Vector3.zero;
		Vector3 rotation = flower.transform.localRotation.eulerAngles;
		rotation.x = 0;
		rotation.y = 0;
		rotation.z = 0;
		flower.transform.localRotation = Quaternion.Euler(rotation);

	}

	public void StartGrowing()
	{
		isGrowing = true;
	}

	void StageOneGrowth()
	{
		flowerScale = flower.transform.localScale;
		flowerScale.y += Time.deltaTime * growthRate;
		if (flowerScale.x < stageOneHeightLimit / 1.25f)
		{
			flowerScale.x += Time.deltaTime * growthRate;
			flowerScale.z += Time.deltaTime * growthRate;
		}
		flower.transform.localScale = flowerScale;

		if (flowerScale.y >= stageOneHeightLimit)
		{
			inStageOneGrowth = false;
			inStageTwoGrowth = true;
			isGrowing = false;
		}
	}

	void StageTwoGrowth()
	{
		flowerScale = flower.transform.localScale;
		flowerScale.y += Time.deltaTime * growthRate;
		flowerScale.x += Time.deltaTime * growthRate * stageTwoReducer;
		flowerScale.z += Time.deltaTime * growthRate * stageTwoReducer;
		flower.transform.localScale = flowerScale;

		if (flowerScale.y >= stageTwoHeightLimit)
		{
			FinishGrowing();
		}
	}

	public void FinishGrowing()
	{
		inStageTwoGrowth = false;
		isGrowing = false;
		SetFlowerComponents (true);
		flower.transform.SetParent (GameObject.Find("Flowers").transform);
		flower.GetComponent<Flori_Flower> ().SetCanBePickedTo (true);
	}

	public int GetWaterDropsToBloom()
	{
		return waterDropsToBloom;
	}

	public bool IsGrowing()
	{
		return isGrowing;
	}

	void SetFlowerComponents(bool on)
	{
		if (!on)
		{
			flower.GetComponent<Rigidbody> ().isKinematic = true;
		}

		flower.GetComponent<CapsuleCollider> ().enabled = on;
		flower.GetComponent<VRTK_InteractableObject> ().isGrabbable = on;

		foreach (MeshCollider collider in flower.GetComponentsInChildren<MeshCollider>())
		{
			collider.enabled = on;
		}
	}

	public GameObject GetFlower()
	{
		return flower;
	}
}
