using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Flori_Seed : MonoBehaviour {

//	[Tooltip("Signifies if flower is currently growing")]
	bool _isGrowing;
	public bool isGrowing 
	{
		get 
		{
			return _isGrowing;
		}
		set
		{
			_isGrowing = value;
			SetCanPickFlowerTo (!value);			
		}
	}

	[Header("Growth Variables")]
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

	[Tooltip("Flower is currently set to grow until its first height limit")]
	[HideInInspector] public bool inStageOneGrowth = true;
	[Tooltip("Flower has grown to its first height limit and is set to grow until its second and final height limit")]
	[HideInInspector] public bool inStageTwoGrowth = false;
		

	GameObject flower;
	Vector3 flowerScale;
	bool canPickFlower = false;

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
	public void Sprout(Color stemColor)
	{
		flower = (GameObject)Instantiate (flowerModel, transform);
		flower.name = flower.name.TrimEnd (new char[] {'(', 'C', 'l', 'o', 'n', 'e', ')' });

		ColorFlower (flower, stemColor);
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
			SetCanPickFlowerTo (true);
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

	public void SetCanPickFlowerTo (bool state)
	{
		canPickFlower = state;
		SetFlowerComponents (state);
		flower.GetComponent < Flori_Flower> ().SetCanBePickedTo(state);
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

	bool CanPickFlower()
	{
		return canPickFlower;
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

	Color GetSeedColor()
	{
		return GetComponent<MeshRenderer> ().material.color;
	}

	public void ColorFlower(GameObject flower, Color stemColor)
	{
		Flori_Flower flori = flower.GetComponent<Flori_Flower> ();
		flori.SetBloomColor (GetSeedColor());
        //flori.SetStamenColor(ChangeColorBrightness(GetSeedColor(), Random.Range(-1, 1) * 0.4f));
        int colorChoice = Random.Range(0, 2); //return value 0 or 1
        if(colorChoice == 0)
        {
            Color yellow = new Color(1F, 0.92F, 0.016F, 1F);
            flori.SetStamenColor(yellow);
        }
        else
        {
            Color white = new Color(255F, 255F, 255F, 1F);
            flori.SetStamenColor(white);
        }     
        //		flori.SetLeavesColor ();
        if (stemColor != Color.green) 
		{
			flori.SetStemColor(stemColor);	
		}
	}

	// Taken from http://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
	public static Color ChangeColorBrightness(Color color, float correctionFactor)
	{
		float red = (float)color.r;
		float green = (float)color.g;
		float blue = (float)color.b;

		if (correctionFactor < 0)
		{
			correctionFactor = 1 + correctionFactor;
			red *= correctionFactor;
			green *= correctionFactor;
			blue *= correctionFactor;
		}
		else
		{
			red = (255 - red) * correctionFactor + red;
			green = (255 - green) * correctionFactor + green;
			blue = (255 - blue) * correctionFactor + blue;
		}

		return new Color(red, green, blue, color.a);
	}

}
