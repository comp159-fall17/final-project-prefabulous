using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTK_WateringCan : MonoBehaviour {

	[Header("Testing Materials")]
	[Tooltip("Material the test can shows during its active range")]
	public Material inRangeMaterial;
	[Tooltip("Material the test can shows during its inactive range")]
	public Material outOfRangeMaterial;

	[Header("Active Range Variables")]
	[Tooltip("Angle at which the watering can starts pouring water")]
	[Range(45f, 75f)]
	public float activationAngle = 60f;
	[Tooltip("Integer maximum of water level variable")]
	[Range(0f, 100f)]
	public int maximumWaterLevel = 10;


	MeshRenderer render;
	float lastAngle;
	int waterLevel;

	// Use this for initialization
	void Start () {

		render = GetComponent<MeshRenderer> ();
		lastAngle = transform.eulerAngles.z;
		waterLevel = 0; // initialize at empty?
	}
	
	// Update is called once per frame
	void Update () {

		float currentAngle = Mathf.Floor (transform.eulerAngles.z);

		if (lastAngle != currentAngle)
		{
			// normalize rotation angle in case the can has somehow been flipped multiple times
			while (currentAngle > 360f) 
			{
				currentAngle -= 360f;
			}

			if (currentAngle > activationAngle && currentAngle < 120f)
			{
				if (render.material != inRangeMaterial) 
				{
					render.material = inRangeMaterial;
				}
			}
			else
			{
				if (render.material != outOfRangeMaterial) 
				{
					render.material = outOfRangeMaterial;
				}
			}
			lastAngle = currentAngle;
		}	

	}

	void PourWater()
	{
		if (waterLevel != 0)
		{
			waterLevel--;
		} 
		else
		{
			Debug.Log ("Watering Can is empty");
			// TODO: Add a visual signal that the can is empty
		}
	}

	// add a volume of water to the can
	public void AddWaterOf(int volume)
	{
		waterLevel += volume;
		waterLevel = Mathf.Clamp (waterLevel, 0, maximumWaterLevel);
	}

	// set the water level directly - clamped to avoid overflow
	public void SetWaterLevelTo(int newLevel)
	{
		waterLevel = newLevel;
		waterLevel = Mathf.Clamp(waterLevel, 0 , maximumWaterLevel);
	}

	void UpdateWaterVisuals()
	{
		// TODO: Update a physical water level within the can
	}

}
