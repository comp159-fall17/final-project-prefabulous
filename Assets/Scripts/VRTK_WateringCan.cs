using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRTK_WateringCan : MonoBehaviour {
    private enum Axis { X_POSITIVE, X_NEGATIVE, Z_POSITIVE, Z_NEGATIVE };

	[Header("Testing Materials")]
	[Tooltip("Material the test can shows during its active range")]
	public Material inRangeMaterial;
	[Tooltip("Material the test can shows during its inactive range")]
    public Material outOfRangeMaterial;
    [Tooltip("Material the test can shows during its spilling range")]
    public Material spillingMaterial;
    [Tooltip("Text to display remaining amount of water in watering can")]
    public Text displayWaterAmount;

	[Header("Active Range Variables")]
	[Tooltip("Angle at which the watering can starts pouring water")]
	[Range(15f, 175f)]
    public float pouringAngle = 60f;
    [Tooltip("Specifies the direction the can should be rotated to produce water.")]
    [SerializeField]
    private Axis pouringAxis = Axis.Z_POSITIVE;
    [Tooltip("Specifies whether or not spilling is enabled")]
    public bool allowSpilling = true;
    [Tooltip("Angle at which the watering can should starting spilling water")]
    [Range(15f, 175f)]
    public float spillingAngle = 95f;

    [Header("Water Level Variables")]
    [Tooltip("Integer maximum of water level variable")]
    [Range(0f, 100f)]

    public int maximumWaterLevel = 100;
    [Tooltip("Water level when the can is spawned")]
    [Range(0f, 100f)]
    public int startingWaterLevel = 10;
    [Tooltip("Time interval between water level decrements when pouring")]
    [Range(0f, 100f)]
    public float pouringInterval = 2f;
    [Tooltip("Time interval between water level decrements when spilling")]
    [Range(0f, 100f)]
    public float spillingInterval = 0.5f;

	MeshRenderer render;
    int waterLevel;
    float timeCounter = 0;
    bool wateringCanIsActive;
    bool wateringCanIsSpilling;

    // Use this for initialization
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        waterLevel = Mathf.Min(maximumWaterLevel, startingWaterLevel);
        displayWaterAmount.text = waterLevel.ToString();
        wateringCanIsActive = false;
    }
	
	// Update is called once per frame
    void Update()
    {
        // TODO: See if there is an equivalent VRTK method to Update, which is only called when the object is interacted with.
        // TODO: Discuss wether it is better to compare previous angle to avoid unnecessary calls of CanIsTipped()
        wateringCanIsActive = CanIsPouring();
        wateringCanIsSpilling = CanIsSpilling();
        if (wateringCanIsSpilling) {
            if (render.material != spillingMaterial)
            {
                render.material = spillingMaterial;
            }
        } else if (wateringCanIsActive)
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

    //Converts an angle in the range of 0..360 to the range -180..180
    private float ConvertToSignedAngleRange(float angle) {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return angle;
    }

    //Returns true if the can is positioned to pour water
    public bool CanIsPouring()
    {
        int signedDirection = 1;
        float magnitudeOfAngle;
        if (pouringAxis == Axis.X_NEGATIVE || pouringAxis == Axis.Z_NEGATIVE)
        {
            signedDirection = -1;
        }
        if (pouringAxis == Axis.X_NEGATIVE || pouringAxis == Axis.X_POSITIVE)
        {
            magnitudeOfAngle = transform.eulerAngles.x;
        }
        else
        {
            magnitudeOfAngle = transform.eulerAngles.z;
        }
        magnitudeOfAngle = ConvertToSignedAngleRange(magnitudeOfAngle);

        return magnitudeOfAngle * signedDirection >= pouringAngle;
    }

    //Returns true if the can is positioned to spill water
    public bool CanIsSpilling() {
        float magnitudeOfX = transform.eulerAngles.x;
        float magnitudeOfZ = transform.eulerAngles.z;
        magnitudeOfX = ConvertToSignedAngleRange(magnitudeOfX);
        magnitudeOfZ = ConvertToSignedAngleRange(magnitudeOfZ);

        return (Mathf.Abs(magnitudeOfX) >= spillingAngle) || (Mathf.Abs(magnitudeOfZ) >= spillingAngle);
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
        if (waterLevel != 0 && wateringCanIsSpilling) {
            timeCounter += Time.deltaTime;
            if (timeCounter >= spillingInterval)
            {
                PourWater();
                timeCounter = 0;
            }
        } else if (waterLevel != 0 && wateringCanIsActive)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= pouringInterval)
            {
                PourWater();
                timeCounter = 0;
            }
        }
    }
}
