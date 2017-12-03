using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;
using VRTK;

public class RadioKnobController : VRTK_PhysicsRotator
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override float GetNormalizedValue ()
	{
		Debug.Log("GetNormalizedValue, GetValue() = " + GetValue() + " angleLimits.minimum = " + angleLimits.minimum + " angleLimits.maximum = " + angleLimits.maximum);
		return base.GetNormalizedValue ();
	}
}
