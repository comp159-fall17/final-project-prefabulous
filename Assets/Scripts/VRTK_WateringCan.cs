using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRTK_WateringCan : MonoBehaviour
{

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
    [Tooltip("Time interval between water level decrements")]
	[Range(0f, 10f)]
	public float timeInterval = 2f;

    MeshRenderer render;
    float lastAngle;
    int waterLevel;
    float timeCounter = 0;
    bool wateringCanIsActive;
    // Use this for initialization
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        lastAngle = transform.eulerAngles.z;
        waterLevel = 10; // initialize at empty?
        displayWaterAmount.text = waterLevel.ToString();
        wateringCanIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        float currentAngle = Mathf.Floor(transform.eulerAngles.z);

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
                    timeCounter = 0;
                    wateringCanIsActive = false;
                }
            }
            lastAngle = currentAngle;
        }
        UpdateWaterLevel();
    }

    //Decrements water, or displays debug log if the can is empty
    void PourWater()
    {
        if (waterLevel != 0)
        {
            waterLevel--;
            displayWaterAmount.text = waterLevel.ToString();
        }
        else
        {
            Debug.Log("Watering Can is empty");
            // TODO: Add a visual signal that the can is empty
        }
    }

    // add a volume of water to the can
    public void AddWaterOf(int volume)
    {
        waterLevel += volume;
        waterLevel = Mathf.Clamp(waterLevel, 0, maximumWaterLevel);
    }

    // set the water level directly - clamped to avoid overflow
    public void SetWaterLevelTo(int newLevel)
    {
        waterLevel = newLevel;
        waterLevel = Mathf.Clamp(waterLevel, 0, maximumWaterLevel);
    }

    void UpdateWaterVisuals()
    {
        // TODO: Update a physical water level within the can
    }

    //Counts down the time interval amount. When the amount is under 0 then reset the time interval.
    void UpdateWaterLevel()
    {
        if (waterLevel != 0 && wateringCanIsActive)
        {
			timeCounter += Time.deltaTime;
            if (timeCounter >= timeInterval)
            {
                PourWater();
                timeCounter = 0;
            }
        }
    }
}
