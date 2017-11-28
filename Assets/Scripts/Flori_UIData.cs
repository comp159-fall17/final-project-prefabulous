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

}
