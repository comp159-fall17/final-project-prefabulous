using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRTK_WateringCan : MonoBehaviour {

<<<<<<< HEAD
    [Header("Testing Materials")]
    [Tooltip("Material the test can shows during its active range")]
    public Material inRangeMaterial;
    [Tooltip("Material the test can shows during its inactive range")]
    public Material outOfRangeMaterial;
    [Tooltip("Text to display remaining amount of water in watering can")]
    public Text displayWaterAmount;

    [Header("Active Range Variables")]
    [Tooltip("Angle at which the watering can starts pouring water")]
    [Range(45f, 75f)]
    public float activationAngle = 60f;
    [Tooltip("Integer maximum of water level variable")]
    [Range(0f, 100f)]
    public int maximumWaterLevel = 10;
    [Tooltip("Time interval for watering")]
    public float timeInterval;

    MeshRenderer render;
=======
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
>>>>>>> 3fb15295d9b4b4ff98e7c5ea81a0ca89aa28b886
	float lastAngle;
	int waterLevel;
    float timeIntervalStart = 0;
    bool wateringCanIsActive;
	// Use this for initialization
	void Start () {
        render = GetComponent<MeshRenderer> ();
		lastAngle = transform.eulerAngles.z;
<<<<<<< HEAD
		waterLevel = 10; // initialize at empty?
        displayWaterAmount.text = "Remaining \n Water " + waterLevel.ToString();
        wateringCanIsActive = false;
    }

    // Update is called once per frame
    void Update () {
=======
		waterLevel = 0; // initialize at empty?

	}
	
	// Update is called once per frame
	void Update () {
>>>>>>> 3fb15295d9b4b4ff98e7c5ea81a0ca89aa28b886

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
                    wateringCanIsActive = true;
                }
			}
			else
			{
				if (render.material != outOfRangeMaterial) 
				{
					render.material = outOfRangeMaterial;
                    timeIntervalStart = 0;
                    wateringCanIsActive = false;
                }
            }
			lastAngle = currentAngle;
		}
        TimeInterval();
    }

    //Decrements water, or displays debug log if the can is empty
    void PourWater()
	{
		if (waterLevel != 0)
		{
			waterLevel--;
            displayWaterAmount.text = "Remaining \n Water " + waterLevel.ToString();
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

    //Counts down the time interval amount. When the amount is under 0 then reset the time interval.
    void TimeInterval()
    {
        if (waterLevel != 0 && wateringCanIsActive)
        {
            timeIntervalStart += Time.deltaTime;
            if(timeIntervalStart >= timeInterval)
            {
                PourWater();
                timeIntervalStart = 0;
            }
        }
    }
}
