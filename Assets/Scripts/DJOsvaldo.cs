﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJOsvaldo : MonoBehaviour {

	static AudioSource radio;
	static AudioSource EffectsSource;
	public AudioClip[] soundEffects;
	public AudioClip[] soundtracks;
	public AudioSource effectsSource;

	public static List<AudioClip> Beats = new List<AudioClip>();
	public static Dictionary<string, AudioClip> Effects = new Dictionary<string, AudioClip> ();
	static bool isPlaying;

	void Awake () {

		radio = GetComponent<AudioSource> ();
		EffectsSource = effectsSource;

		foreach (AudioClip soundtrack in soundtracks) 
		{
			if (!Beats.Contains (soundtrack)) 
			{
				Beats.Add (soundtrack);
			}
		}

		if (!Effects.ContainsKey ("click")) 
		{
			Effects.Add ("click", soundEffects[0]);
		}

    }

	public static void PlayEffectAt(string name, float level = 0.35f)
	{
		if (Effects.ContainsKey(name)) 
		{
			EffectsSource.volume = level;
			EffectsSource.clip = Effects [name];
			EffectsSource.Play ();
		}
	}

	/// <summary>
	/// Changes the sound track to a specific clip entry in `Beats` dictionary. Calling this with silencing == true will silence the radio's music.
	/// </summary>
	/// <param name="station">Station.</param>
	/// <param name="silencing">If set to <c>true</c> silencing.</param>
	public static void ChangeSoundTrackTo(int station, bool silencing = false)
	{
		if (silencing)
		{
			radio.Stop ();
			radio.clip = null;
			return;
		}
		radio.clip = Beats[station];
		radio.Play ();

	}

	/// <summary>
	/// Changes the music volume to a value that must be between 0 and 1f.
	/// </summary>
	/// <param name="soundLevel">Sound level.</param>
    public static void ChangeMusicVolume(float soundLevel)
    {
		radio.volume = Mathf.Clamp01(soundLevel);
    }
}
