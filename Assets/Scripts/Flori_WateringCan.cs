using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flori_WateringCan : MonoBehaviour {


	public static Flori_WateringCan Instance;
    private enum Axis { X_POSITIVE, X_NEGATIVE, Z_POSITIVE, Z_NEGATIVE };

	[Header("Testing Variables")]
	[Tooltip("Highlights can when spilling or pouring, and displays water level in text.")]
	public bool testingCan = false;
	[Tooltip("Material the test can shows during its active range.")]
	public Material inRangeMaterial;
	[Tooltip("Material the test can shows during its inactive range.")]
    public Material outOfRangeMaterial;
    [Tooltip("Material the test can shows during its spilling range.")]
    public Material spillingMaterial;
    [Tooltip("Text to display remaining amount of water in watering can.")]
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

	[Header("Visual Water Level Variables")]
	[Tooltip("Visual water level within the can that indicates its programmatic value")]
	public GameObject visualLevel;
	[Tooltip("The bounds along the can's local y-axis which dictate the minimum and maximum position of its water level")]
	public Vector2 visualBounds;

	MeshRenderer render;
	ParticleSystem particles;
	int _waterLevel;
	int waterLevel {
		get { return _waterLevel; }
		set 
		{
			_waterLevel = value;
			SetVisualLevel (value);
			if (testingCan) 
			{
				displayWaterAmount.text = waterLevel.ToString ();
			}
		}
	}
    float timeCounter = 0;
    bool wateringCanIsActive;
    bool wateringCanIsSpilling;
	bool isSubmerged = false;

    // Use this for initialization
    void Start()
    {
		if (Instance == null) 
		{
			Instance = this;
		}
		else if (Instance == this) 
		{
			Destroy (gameObject);
		}

		render = GetComponent<MeshRenderer>();
		particles = GetComponentInChildren<ParticleSystem> ();
        waterLevel = Mathf.Min(maximumWaterLevel, startingWaterLevel);
        wateringCanIsActive = false;
		displayWaterAmount.gameObject.SetActive (testingCan);

    }
	
	// Update is called once per frame
    void Update()
    {
        // TODO: See if there is an equivalent VRTK method to Update, which is only called when the object is interacted with.
        // TODO: Discuss wether it is better to compare previous angle to avoid unnecessary calls of CanIsTipped()
        wateringCanIsActive = CanIsPouring();
        wateringCanIsSpilling = CanIsSpilling();
		if (wateringCanIsSpilling && !IsEmpty())
		{
			if (testingCan && render.material != spillingMaterial) 
			{
				render.material = spillingMaterial;
			}
		} 
		else if (wateringCanIsActive && !IsEmpty())
        {
			if (testingCan && render.material != inRangeMaterial) 
			{
				render.material = inRangeMaterial;
			}
			TurnParticlesOn ();
        }
		else
        {
			if (testingCan && render.material != outOfRangeMaterial) 
			{
				render.material = outOfRangeMaterial;
			}
			TurnParticlesOff ();
        }
        UpdateWaterLevel();
	}
		
	/// <summary>
	/// Decrements water, or displays debug log if the can is empty
	/// </summary>
    void PourWater()
    {
		if (waterLevel != 0 && !IsSubmerged())
        {
            waterLevel--;
        }
        else
        {
            Debug.Log("Watering Can is empty or submerged");
            // TODO: Add a visual signal that the can is empty
        }
    }
		
	/// <summary>
	/// Converts to signed angle range of 0..360 to the range -180..180.
	/// </summary>
	/// <returns>The to signed angle range.</returns>
	/// <param name="angle">Angle.</param>
    float ConvertToSignedAngleRange(float angle) {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return angle;
    }

    /// <summary>
    /// Determines whether this instance can is pouring.
    /// </summary>
    /// <returns><c>true</c> if this instance can is pouring; otherwise, <c>false</c>.</returns>
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

	/// <summary>
	/// Determines whether this instance can is spilling.
	/// </summary>
	/// <returns><c>true</c> if this instance can is spilling; otherwise, <c>false</c>.</returns>
    public bool CanIsSpilling() 
	{
        float magnitudeOfX = transform.eulerAngles.x;
        float magnitudeOfZ = transform.eulerAngles.z;
        magnitudeOfX = ConvertToSignedAngleRange(magnitudeOfX);
        magnitudeOfZ = ConvertToSignedAngleRange(magnitudeOfZ);

        return (Mathf.Abs(magnitudeOfX) >= spillingAngle) || (Mathf.Abs(magnitudeOfZ) >= spillingAngle);
    }
		
	/// <summary>
	/// Adds a volume of water to the can.
	/// </summary>
	/// <param name="volume">Volume.</param>
    public void AddWaterOf(int volume)
    {
        waterLevel += volume;
        waterLevel = Mathf.Clamp(waterLevel, 0, maximumWaterLevel);
    }

	/// <summary>
	/// Sets the water level to a new level directly - clamped to avoid overflow.
	/// </summary>
	/// <param name="newLevel">New level.</param>
    public void SetWaterLevelTo(int newLevel)
    {
        waterLevel = newLevel;
        waterLevel = Mathf.Clamp(waterLevel, 0, maximumWaterLevel);
    }

	/// <summary>
	/// Counts down the time interval amount. When the amount is under 0 then reset the time interval.
	/// </summary>
    void UpdateWaterLevel()
    {
        if (waterLevel != 0 && wateringCanIsSpilling) 
		{
            timeCounter += Time.deltaTime;
            if (timeCounter >= spillingInterval)
            {
                PourWater();
                timeCounter = 0;
            }
        } 
		else if (waterLevel != 0 && wateringCanIsActive)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= pouringInterval)
            {
                PourWater();
                timeCounter = 0;
            }
        }
    }

	/// <summary> 
	/// Set visual water level inside the can. Is automatically scaled to the maximum water level 
	/// </summary>
	void SetVisualLevel(float height)
	{
		Vector3 newLevel = visualLevel.transform.localPosition;
		newLevel.y = Mathf.Clamp((height / maximumWaterLevel * (visualBounds.y - visualBounds.x)) + visualBounds.x, visualBounds.x, visualBounds.y);
		visualLevel.transform.localPosition = newLevel;
	}

	/// <summary> 
	/// Returns true if waterlevel variable is 0 
	/// </summary>
	public bool IsEmpty()
	{
		return waterLevel == 0;
	}

	public bool IsFull()
	{
		return waterLevel == maximumWaterLevel;
	}

	/// <summary>
	/// Turns the particles to a specific state.
	/// </summary>
	/// <param name="on">If set to <c>true</c> on.</param>
	void TurnParticlesOn()
	{
		if (waterLevel != 0 && !particles.gameObject.activeInHierarchy)
		{
			particles.gameObject.SetActive (true);
		}
	}

	void TurnParticlesOff()
	{
		if (particles.gameObject.activeInHierarchy)
		{
			particles.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Determines whether this instance is submerged in a well.
	/// </summary>
	/// <returns><c>true</c> if this instance is submerged; otherwise, <c>false</c>.</returns>
	public bool IsSubmerged()
	{
		return isSubmerged;
	}

	/// <summary>
	/// Sets the is submerged bool of this instance.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void SetIsSubmerged(bool state)
	{
		isSubmerged = state;
	}

}
