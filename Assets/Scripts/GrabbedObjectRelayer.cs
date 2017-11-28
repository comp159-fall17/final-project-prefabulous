using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VOA = VRTK.VRTK_ObjectAppearance;

public class GrabbedObjectRelayer : VRTK_InteractGrab {

	[Header("Grabbed Object UI")]
	[Tooltip("Canvas prefab that gets overlayed when an object is grabbed")]
	public GameObject itemDescriptionPrefab;
	[Tooltip("Flip text to other side of objects")]
	public bool leftHand = false;

	GameObject currentItemDescription;

	protected override void PerformGrabAttempt (GameObject objectToGrab)
	{
		base.PerformGrabAttempt (objectToGrab);
		if (objectToGrab != null) 
		{
			Debug.Log (objectToGrab.name);
			CreateItemText (objectToGrab);
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
		itemDescription.transform.localPosition = GetTextPosition (itemDescription);
		itemDescription.transform.SetGlobalScale (GetTextScale(itemDescription));
		if (leftHand)
		{
			if (!itemDescription.GetComponentInParent<Flori_UIData>().doNotInvert)
			{
				RotateText (itemDescription, 180f);
			}
		}

		Text descriptionText = itemDescription.GetComponentInChildren<Text> ();
		descriptionText.text = forObject.name;
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

	// Tries to retrieve and return VRTK_UIData position variable
	Vector3 GetTextPosition(GameObject text)
	{
		try 
		{
			Flori_UIData UIData = text.GetComponentInParent<Flori_UIData>();
			if (!leftHand || UIData.doNotInvert) 
			{
				return UIData.textPosition;
			}
			else
			{
				Vector3 textPosition = UIData.textPosition;
				textPosition.z *= -1f;
				return textPosition;
			}
		} 
		catch 
		{
			if (!leftHand) 
			{
				return new Vector3 (0f, -0.4f, -0.7f);
			}
			else
			{
				return new Vector3 (0f, -0.4f, 0.7f);
			}
		}
	}

	// Tries to retrieve and return VRTK_UIData scale variable
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

	// Rotate GameObject (meant for description objects) by degrees and in clockwise direction by default
	void RotateText(GameObject description, float degrees, int direction = -1)
	{
		Vector3 descriptionRotation = description.transform.eulerAngles;
		descriptionRotation.y += direction * degrees;
		description.transform.eulerAngles = descriptionRotation;
	}

}
