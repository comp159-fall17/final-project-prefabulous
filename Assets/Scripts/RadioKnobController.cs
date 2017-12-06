using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class RadioKnobController : ControlReactor
{
	[Header("RadioKnobController Options")]
	public bool isVolumeKnob;

	float lastValue;

	protected override void ValueChanged (object sender, VRTK.Controllables.ControllableEventArgs e)
	{
		base.ValueChanged (sender, e);
		if (isVolumeKnob) 
		{
			if (e.value != lastValue)	
			{
				DJOsvaldo.ChangeMusicVolume (e.value / 10f);
			} 
		} 
		else
		{
			if (e.value != lastValue)	
			{
				DJOsvaldo.ChangeSoundTrackTo ((int)e.value);
			}
		}
			
		lastValue = e.value;
	}

}
