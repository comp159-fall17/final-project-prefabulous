using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class GrabbedObjectRelayer : VRTK_InteractGrab {

	[Header("Grabbed Object UI")]
	[Tooltip("Canvas prefab that gets overlayed when an object is grabbed")]
	public GameObject itemDescriptionPrefab;

	protected override void PerformGrabAttempt (GameObject objectToGrab)
	{
		base.PerformGrabAttempt (objectToGrab);
		if (objectToGrab != null) 
		{
			Debug.Log (objectToGrab.name);
			CreateItemText (objectToGrab);
		}
	}

	// Creates a world space text UI that shows the object's name
	void CreateItemText(GameObject forObject)
	{
		GameObject itemDescription = Instantiate (itemDescriptionPrefab, forObject.transform);
		Text descriptionText = itemDescription.GetComponentInChildren<Text> ();
		descriptionText.text = forObject.name;
	}


}
