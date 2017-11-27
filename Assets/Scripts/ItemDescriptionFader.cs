using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class ItemDescriptionFader : MonoBehaviour {


	[Header("Visibility Controls")]
	[Tooltip("Distance threshold for text fading")]
	[Range(0f, 2f)]
	public float activeDistance = 0.5f;
	[Tooltip("Speed at which the text fades in and out")]
	[Range(0.01f, 2f)]
	public float fadeSpeed = 0.75f;

	Text descriptionText;
	Color tempColor;

	// Use this for initialization
	void Start () {

		descriptionText = GetComponentInChildren<Text> ();

	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (DistanceFromCamera() <= activeDistance && descriptionText.color.a == 0) 
		{
			Debug.Log ("Text is visible");
			StartCoroutine ("FadeTextIn");
		}
		else if (DistanceFromCamera() > activeDistance && descriptionText.color.a == 1)
		{
			Debug.Log ("Text is hiding");
			StartCoroutine ("FadeTextOut");
		}


	}

	float DistanceFromCamera()
	{
		return Vector3.Distance (Camera.main.transform.position, transform.parent.position);
	}

	IEnumerator FadeTextIn()
	{
		while (descriptionText.color.a < 1)
		{
			tempColor = descriptionText.color;
			tempColor.a += Time.deltaTime * fadeSpeed;

			descriptionText.color = tempColor;
			yield return null;
		}
		SetTextOpacityTo (1f);

		yield return null;
	}

	IEnumerator FadeTextOut()
	{
		while (descriptionText.color.a > 0)
		{
			tempColor = descriptionText.color;
			tempColor.a -= Time.deltaTime * fadeSpeed;

			descriptionText.color = tempColor;
			yield return null;
		}
		SetTextOpacityTo (0f);

		yield return null;
	}

	void SetTextOpacityTo(float alpha)
	{
		tempColor.a = alpha;
		descriptionText.color = tempColor;
	}

}
