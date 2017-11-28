using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_UIData : MonoBehaviour {

	[Header("UI Text Variables")]
	[Tooltip("Local (to parent) position of UI text description")]
	public Vector3 textPosition;
	[Tooltip("Global (to parent) scale of UI text description")]
	public Vector3 textScale;
	[Tooltip("Will not rotate text to opposite side of object if selected")]
	public bool doNotInvert = false;

	[Header("UI Information")]
	[Tooltip("Information for UI to display")]
	public string itemInfo = "";
	[Tooltip("Show item info for this object on activation")]
	public bool showItemInfo = false;

	// Set item info to a new string
	public void SetItemDescription(string newDescription)
	{
		itemInfo = newDescription;
	}

	// Set item info to an integer
	public void SetItemDescription(int newDescription)
	{
		itemInfo = newDescription.ToString();
	}

}
