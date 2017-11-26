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

	MeshRenderer render;
	float lastAngle;

	// Use this for initialization
	void Start () {

		render = GetComponent<MeshRenderer> ();
		lastAngle = transform.eulerAngles.z;
	
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
}
