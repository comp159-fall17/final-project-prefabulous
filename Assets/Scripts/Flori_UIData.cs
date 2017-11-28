﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_UIData : MonoBehaviour {

	[Header("UI Text Variables")]
	[Tooltip("Local (to parent) position of UI text")]
	public Vector3 descriptionPosition;
	[Tooltip("Global (to parent) scale of UI text")]
	public Vector3 textScale;

	[Header("Text Properties")]
	[Tooltip("Local (to parent) position of UI text object name")]
	public Vector3 namePosition;
	[Tooltip("Local (to parent) position of UI text object info")]
	public Vector3 infoPosition;
	[Tooltip("Global (to parent) scale of UI text object info")]
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