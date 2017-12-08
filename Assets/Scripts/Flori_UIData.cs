using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_UIData : MonoBehaviour {

	[Header("UI Text Variables")]
	[Tooltip("Disable UI description for this object.")]
	public bool disableDescription = true;
	[Tooltip("Local (to parent) position of UI text.")]
	public Vector3 descriptionPosition;
	[Tooltip("Global (to parent) scale of UI text.")]
	public Vector3 textScale = new Vector3(0.005f, 0.005f, 0.01f);

	[Header("Text Properties")]
	[Tooltip("Local (to parent) position of UI text object name.")]
	public Vector3 namePosition;
	[Tooltip("Local (to parent) position of UI text object info.")]
	public Vector3 infoPosition;

	[Header("UI Information")]
	[Tooltip("Information for UI to display.")]
	public string itemInfo = "";
	[Tooltip("Show item info for this object on activation.")]
	public bool showItemInfo = false;

	[Header("Curved UI Settings")]
	[Tooltip("Override the default UI curve angle of -90.")]
	public bool overrideDefaultBendAngle = false;
	[Tooltip("Angle at which this item description curves.")]
	[Range(-360f, 360f)]
	public int bendAngle = -90;

	[Header("Fading Distance")]
	[Tooltip("If true, the default fade distance is overrided with the one below.")]
	public bool overrideFadeDistance = false;
	[Tooltip("The overridden distance that the text fades in at.")]
	[Range(0.4f, 2f)]
	public float fadeDistance = 0.5f;

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
