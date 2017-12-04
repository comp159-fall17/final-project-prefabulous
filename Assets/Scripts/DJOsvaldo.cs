using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJOsvaldo : MonoBehaviour {

	static AudioSource radio;
	static AudioSource effectsSource;
	public AudioClip[] soundEffects;
	public AudioClip[] soundtracks;

	public static Dictionary<string, AudioClip> Beats = new Dictionary<string, AudioClip> ();
	public static Dictionary<string, AudioClip> Effects = new Dictionary<string, AudioClip> ();
	static bool isPlaying;

	// Use this for initialization
	void Start () {

		radio = GetComponent<AudioSource> ();
		effectsSource = GetComponentInChildren<AudioSource> ();

		Beats.Add ("Outside", soundtracks[0]);
		Beats.Add ("Clarinet", soundtracks[1]);

//		Effects.Add ("<Title>", soundEffects[0]); // sample for effects

    }

	public static void PlayEffectAt(string name, float level = 0.35f)
	{
		if (Effects.ContainsKey(name) && !radio.isPlaying) 
		{
			effectsSource.volume = level;
			effectsSource.clip = Effects [name];
			effectsSource.Play ();
		}
	}

	/// <summary>
	/// Changes the sound track to a specific clip entry in `Beats` dictionary. Calling this with silencing == true will silence the radio's music.
	/// </summary>
	/// <param name="station">Station.</param>
	/// <param name="silencing">If set to <c>true</c> silencing.</param>
	public static void ChangeSoundTrackTo(string station, bool silencing = false)
	{
		if (silencing)
		{
			radio.Stop ();
			radio.clip = null;
			return;
		}
		if (Beats.ContainsKey(station))
		{
			radio.clip = Beats [station];
			radio.Play ();
		}

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
