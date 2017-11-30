using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Seed : MonoBehaviour {

	[Header("Growth Variables")]
	[Tooltip("Signifies if flower is currently growing")]
	public bool isGrowing = false;
	[Tooltip("Height limit for the first stage of flower growth")]
	public float stageOneHeightLimit = 350f;
	[Tooltip("Height limit for the second stage of flower growth")]
	public float stageTwoHeightLimit = 600f;
	[Tooltip("Amount of water drops needed to trigger growth")]
	[Range(1, 100)]
	public int waterDropsToBloom = 5;

	[Header("Flower Model")]
	[Tooltip("The flower prefab the seed will sprout once watered")]
	public GameObject flower;

	[Header("Testing Variables")]
	[Tooltip("Flower is currently set to grow until its first height limit")]
	public bool inStageOneGrowth = true;
	[Tooltip("Flower has grown to its first height limit and is set to grow until its second and final height limit")]
	public bool inStageTwoGrowth = false;

    float floweringSpeed = 1.0f;
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
        Destroy(this.GetComponent<Rigidbody>());
    }

    public void StartGrowing()
    {
        isGrowing = true;
    }

    void StageOneGrowth()
    {
        flowerScale = flower.transform.localScale;
        flowerScale.y += Time.deltaTime * floweringSpeed;
        if (flowerScale.x < 200f)
        {
            flowerScale.x += Time.deltaTime * floweringSpeed / 3.0f;
            flowerScale.z += Time.deltaTime * floweringSpeed / 3.0f;
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
        flowerScale.y += Time.deltaTime * floweringSpeed;
        flowerScale.x += Time.deltaTime * floweringSpeed / 5.0f;
        flowerScale.z += Time.deltaTime * floweringSpeed / 5.0f;
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
        flower.GetComponent<Flower>().SetCanBePicked(true);
    }

	public int GetWaterDropsToBloom()
	{
		return waterDropsToBloom;
	}

	public bool IsGrowing()
	{
		return isGrowing;
	}

}
