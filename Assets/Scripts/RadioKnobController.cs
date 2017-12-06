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
		DJOsvaldo.ChangeSoundTrackTo("Outside" );
	}

	// Update is called once per frame
	void Update () {
        //DJOsvaldo.ChangeMusicVolume(GetStepValue(GetValue())/100);
        DJOsvaldo.ChangeMusicVolume(GetStepValue(GetValue()) / 100f);

    }

}
