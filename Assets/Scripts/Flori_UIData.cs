using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_UIData : MonoBehaviour {

	[Header("UI Text Variables")]
	[Tooltip("Local (to parent) position of UI text.")]
	public Vector3 descriptionPosition;
	[Tooltip("Global (to parent) scale of UI text.")]
	public Vector3 textScale;
	[Tooltip("Additional rotation about object in clockwise direction.")]
	[Range(0f, 360f)]
	public float textRotation;

	[Header("Text Properties")]
	[Tooltip("Local (to parent) position of UI text object name.")]
	public Vector3 namePosition;
	[Tooltip("Local (to parent) position of UI text object info.")]
	public Vector3 infoPosition;

	[Header("Left-Handed Text Properties")]
	[Tooltip("Position shift for text object when picked up with left hand")]
	public Vector3 leftHandTextShift;
	[Tooltip("Rotation angle of text from right hand in clockwise direction.")]
	[Range(-360f, 360f)]
	public float leftHandTextRotation;
	[Tooltip("Global (to parent) scale of UI text object info.")]
	public bool doNotInvert = false;

	[Header("UI Information")]
	[Tooltip("Information for UI to display.")]
	public string itemInfo = "";
	[Tooltip("Show item info for this object on activation.")]
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
