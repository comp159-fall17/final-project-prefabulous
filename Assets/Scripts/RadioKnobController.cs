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
        float test = GetAngleFromStepValue(GetStepValue(GetValue()));
        Debug.Log("test " + test);
        ManageSpring(false, restingAngle);
    }
 
    /// <summary>
    /// The GetAngleFromStepValue returns the angle the rotator would be at based on the given step value.
    /// </summary>
    /// <param name="givenStepValue">The step value to check the angle for.</param>
    /// <returns>The angle the rotator would be at based on the given step value.</returns>
    public override float GetAngleFromStepValue(float givenStepValue)
    {
        float normalizedStepValue = VRTK_SharedMethods.NormalizeValue(givenStepValue, stepValueRange.minimum, stepValueRange.maximum);
        Debug.Log("normalizedStepValue " + normalizedStepValue);

        return (controlJoint != null ? Mathf.Lerp(controlJoint.limits.min, controlJoint.limits.max, Mathf.Clamp01(normalizedStepValue)) : 0f);
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
