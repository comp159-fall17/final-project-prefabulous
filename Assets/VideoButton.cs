using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;
using UnityEngine.Video;

public class VideoButton : ButtonReactor {

	public VideoPlayer videoPlayer;

	protected override void MaxLimitReached (object sender, VRTK.Controllables.ControllableEventArgs e)
	{
		base.MaxLimitReached (sender, e);
		DJOsvaldo.PlayEffectAt ("click", 0.04f);
		videoPlayer.time = 0d;
		videoPlayer.Play ();
	}
}
