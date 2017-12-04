using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;
using VRTK;
using VRTK.Controllables;

public class RadioKnobController : VRTK_PhysicsRotator
{
    [Header("RadioKnobController Options")]
    public bool isVolumeKnob;
    // Use this for initialization
    protected VRTK_BaseControllable controllableEvents;

    void Start () {
        //for testing
        DJOsvaldo.PlayClipAt("Outside" , 0.35f);
    }
	
	// Update is called once per frame
	void Update () {

    }

	public override float GetNormalizedValue ()
	{
		Debug.Log("GetNormalizedValue, GetValue() = " + GetValue() + " angleLimits.minimum = " + angleLimits.minimum + " angleLimits.maximum = " + angleLimits.maximum);
        //return base.GetNormalizedValue ();
        return VRTK_SharedMethods.NormalizeValue(GetValue(), angleLimits.minimum, angleLimits.maximum);
    }

    protected override void AttemptMove()
    {
        SetFrictions(grabbedFriction);
        float test = GetStepValue(GetValue())/100;
        Debug.Log("test/100 " + test);
        DJOsvaldo.ChangeMusicVolume(test);
        ManageSpring(false, restingAngle);
    }
 
    
    /// <summary>
    /// The GetStepValue method returns the current angle of the rotator based on the step value range.
    /// </summary>
    /// <param name="currentValue">The current angle value of the rotator to get the Step Value for.</param>
    /// <returns>The current Step Value based on the rotator angle.</returns>
    public override float GetStepValue(float currentValue)
    {
        float step = Mathf.Lerp(stepValueRange.minimum, stepValueRange.maximum, VRTK_SharedMethods.NormalizeValue(currentValue, angleLimits.minimum, angleLimits.maximum));
        return Mathf.Round(step / stepSize) * stepSize;
    }

    /// <summary>
    /// The GetValue method returns the current rotation value of the rotator.
    /// </summary>
    /// <returns>The actual rotation of the rotator.</returns>
    public override float GetValue()
    {
        float currentValue = transform.localEulerAngles[(int)operateAxis];
        return Quaternion.Angle(transform.localRotation, originalLocalRotation) * Mathf.Sign((currentValue > 180f ? currentValue - 360f : currentValue));
    }
}
