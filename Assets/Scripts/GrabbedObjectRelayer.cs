using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VOA = VRTK.VRTK_ObjectAppearance;
using CurvedUI;

public class GrabbedObjectRelayer : VRTK_InteractGrab {

	[Header("Grabbed Object UI")]
	[Tooltip("Canvas prefab that gets overlayed when an object is grabbed")]
	public GameObject itemDescriptionPrefab;
	[Tooltip("Flip text to other side of objects")]
	public bool leftHandGrabbing = false;

	GameObject currentItemDescription;
	Text itemInfo;
	Text itemName;

	protected override void PerformGrabAttempt (GameObject objectToGrab)
	{
		if (objectToGrab.CompareTag("Flower") && objectToGrab.GetComponent<Flori_Flower>().IsAttached() && objectToGrab.GetComponent<Flori_Flower>().CanBePicked())
		{
			Flori_Planter planterToReset;
			foreach (GameObject planter in GameObject.FindGameObjectsWithTag("Planter"))
			{
				if (planter.GetComponent<Flori_Planter>().GetSeedInPlanter() == objectToGrab.GetComponent<Flori_Flower>().GetParentSeed())
				{
					Flori_Seed seedInThisPlanter = planter.GetComponent<Flori_Planter> ().GetSeedInPlanter ();
					planter.GetComponent<VRTK_SnapDropZone> ().ForceUnsnap ();
					Destroy(seedInThisPlanter.gameObject);
					planter.GetComponent<Flori_Planter> ().RemoveCropFrom ();
				}	
			}

			objectToGrab.GetComponent<Rigidbody> ().isKinematic = false;
			objectToGrab.GetComponent<Flori_Flower> ().Detach ();
		}

		base.PerformGrabAttempt (objectToGrab);
		if (objectToGrab != null) 
		{
			Flori_UIData UIData = objectToGrab.GetComponent<Flori_UIData>();
			if (UIData != null && !UIData.disableDescription) 
			{
				CreateItemText (objectToGrab);	
			}

		}

	}

	protected override void AttemptReleaseObject ()
	{
		base.AttemptReleaseObject ();
		DestroyItemText ();
	}

	// Creates a world space text UI that shows the object's name
	void CreateItemText(GameObject forObject)
	{
		GameObject itemDescription = Instantiate (itemDescriptionPrefab, forObject.transform);
		itemDescription.transform.localPosition = GetDescriptionPosition (itemDescription);
		itemDescription.transform.SetGlobalScale (GetTextScale(itemDescription));

		Flori_UIData UIData = itemDescription.GetComponentInParent<Flori_UIData> ();

		HandleTextBending (UIData, itemDescription);

		if (leftHandGrabbing)
		{
			if (!UIData.doNotInvert)
			{
				Debug.Log ("Inverting text");
				RotateText (itemDescription, 180f);
			}
			RotateText (itemDescription, UIData.leftHandTextRotation);
			ShiftText (itemDescription, UIData.leftHandTextShift);
		}

		if (UIData != null) RotateText (itemDescription, UIData.textRotation);

		AssignComponentsOf (itemDescription);

		itemInfo.transform.localPosition = GetInfoPosition (itemInfo.gameObject.transform.parent.gameObject);
		itemName.transform.localPosition = GetNamePosition (itemName.gameObject.transform.parent.gameObject);

		if (itemInfo.GetComponentInParent<Flori_UIData> () != null)
		{
			itemInfo.text = UIData.itemInfo;
		}
		itemName.text = forObject.name;

		currentItemDescription = itemDescription;
	}

	// Destroys the current UI description associated with this controller
	void DestroyItemText()
	{
		if (currentItemDescription != null) 
		{
			Destroy (currentItemDescription);
		}
	}

	Vector3 GetDescriptionPosition(GameObject text)
	{
		try 
		{
			Flori_UIData UIData = text.GetComponentInParent<Flori_UIData>();
			if (!leftHandGrabbing || UIData.doNotInvert) 
			{
				return UIData.descriptionPosition;
			}
			else
			{
				Vector3 textPosition = UIData.descriptionPosition;
				textPosition.z *= -1f;
				return textPosition;
			}
		} 
		catch 
		{
			if (!leftHandGrabbing) 
			{
				return new Vector3 (0f, -0.0f, -0.7f);
			}
			else
			{
				return new Vector3 (0f, -0.0f, 0.7f);
			}
		}
	}

	// Tries to retrieve and return Flori_UIData scale variable
	Vector3 GetTextScale(GameObject text)
	{
		try 
		{
			return text.GetComponentInParent<Flori_UIData>().textScale;
		} 
		catch 
		{
			return new Vector3 (0.005f, 0.005f, 0.01f);
		}
	}

	// Tries to retrieve and return Flori_UIData position variable
	Vector3 GetNamePosition(GameObject text)
	{
		try 
		{
			Flori_UIData UIData = text.GetComponentInParent<Flori_UIData>();
			if (!leftHandGrabbing || UIData.doNotInvert) 
			{
				return UIData.namePosition;
			}
			else
			{
				Vector3 textPosition = UIData.namePosition;
				textPosition.z *= -1f;
				return textPosition;
			}
		} 
		catch 
		{
			if (!leftHandGrabbing) 
			{
				return new Vector3 (0f, -20f, -0.5f);
			}
			else
			{
				return new Vector3 (0f, -20f, 0.5f);
			}
		}
	}

	// Tries to retrieve and return Flori_UIData position variable
	Vector3 GetInfoPosition(GameObject text)
	{
		try 
		{
			Flori_UIData UIData = text.GetComponentInParent<Flori_UIData>();
			if (!leftHandGrabbing || UIData.doNotInvert) 
			{
				return UIData.infoPosition;
			}
			else
			{
				Vector3 textPosition = UIData.infoPosition;
				textPosition.z *= -1f;
				return textPosition;
			}
		} 
		catch 
		{
			if (!leftHandGrabbing) 
			{
				return new Vector3 (0f, -0.4f, -0.7f);
			}
			else
			{
				return new Vector3 (0f, -0.4f, 0.7f);
			}
		}
	}

	/// <summary>
	/// Rotate GameObject (meant for description objects) by degrees and in clockwise direction by default.
	/// </summary>
	/// <param name="description">UI Text Description to rotate.</param>
	/// <param name="direction">Clockwise by default at -1.</param>
	void RotateText(GameObject description, float degrees, int direction = -1)
	{
		Vector3 descriptionRotation = description.transform.eulerAngles;
		descriptionRotation.y += direction * degrees;
		description.transform.eulerAngles = descriptionRotation;
	}

	/// <summary>
	/// Shift GameObject (meant for description objects) by distance vector.
	/// </summary>
	/// <param name="description">Description.</param>
	/// <param name="distance">Distance.</param>
	void ShiftText(GameObject description, Vector3 distance)
	{
		Vector3 location = description.transform.localPosition;
		location += distance;
		description.transform.localPosition = location;
	}

	/// <summary>
	/// Assigns the components of the item description.
	/// </summary>
	/// <param name="itemDescription">Item description.</param>
	void AssignComponentsOf(GameObject itemDescription)
	{
		Text[] descriptionTexts = itemDescription.GetComponentsInChildren<Text>();
		foreach (Text text in descriptionTexts)
		{
			if (text.gameObject.CompareTag("Item Info"))
			{
				itemInfo = text;
			}
			if (text.gameObject.CompareTag("Item Name"))
			{
				itemName = text;
			}
		}
	}

	/// <summary>
	/// Sets custom text bending if override is selected.
	/// </summary>
	/// <param name="UIData">User interface data.</param>
	/// <param name="itemDescription">Item description.</param>
	void HandleTextBending(Flori_UIData UIData, GameObject itemDescription)
	{
		if (UIData.overrideDefaultBendAngle)
		{
			itemDescription.GetComponent<CurvedUISettings> ().Angle = UIData.bendAngle;
		}
	}

}
