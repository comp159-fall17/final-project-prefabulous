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

	Vector3 GetTextPosition(GameObject text)
	{
		try 
		{
			return text.GetComponentInParent<VRTK_UIData>().textPosition;
		} 
		catch 
		{
			return new Vector3 (0f, -0.4f, -0.7f);
		}
	}

	Vector3 GetTextScale(GameObject text)
	{
		try 
		{
			return text.GetComponentInParent<VRTK_UIData>().textScale;
		} 
		catch 
		{
			return new Vector3 (0.005f, 0.005f, 0.01f);
		}
	}


}
