using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;

public class RadioKnobController : VRTK_PhysicsRotator
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override float GetNormalizedValue()
    {
        Debug.Log("GetNormalizedValue, GetValue() = " + GetValue() + " angleLimits.minimum = " + angleLimits.minimum + " angleLimits.maximum = " + angleLimits.maximum);
        return VRTK_SharedMethods.NormalizeValue(GetValue(), angleLimits.minimum, angleLimits.maximum);
    }
}
