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
	public float fadeSpeed = 0.85f;

	List<Text> descriptionTexts;
	Color tempColor;

	// Use this for initialization
	void Start () {

		descriptionTexts = new List<Text>(GetComponentsInChildren<Text> ());
		GameObject UIDataHolder = descriptionTexts [0].gameObject.transform.parent.gameObject;
		if (UIDataHolder.GetComponentInParent<Flori_UIData>() == null || !UIDataHolder.GetComponentInParent<Flori_UIData>().showItemInfo)
		{
			ExcludeInfoText ();
		}
		if (GetComponentInParent<Flori_UIData>() != null && GetComponentInParent<Flori_UIData>().overrideFadeDistance)
		{
			activeDistance = GetComponentInParent<Flori_UIData> ().fadeDistance;
		}

	}
	
	void LateUpdate () {

		if (DistanceFromCamera() <= activeDistance && descriptionTexts[0].color.a == 0) 
		{
			StartCoroutine ("FadeTextIn");
		}
		else if (DistanceFromCamera() > activeDistance && descriptionTexts[0].color.a == 1)
		{
			StartCoroutine ("FadeTextOut");
		}

		transform.rotation = Camera.main.transform.rotation;

	}

	float DistanceFromCamera()
	{
		return Vector3.Distance (Camera.main.transform.position, transform.parent.position);
	}

	IEnumerator FadeTextIn()
	{
		while (descriptionTexts[0].color.a < 1)
		{
			tempColor = descriptionTexts[0].color;
			tempColor.a += Time.deltaTime * fadeSpeed;

			foreach (Text element in descriptionTexts)
			{
				element.color = tempColor;	
			}
			yield return null;
		}
		SetTextOpacityTo (1f);

		yield return null;
	}

	IEnumerator FadeTextOut()
	{
		while (descriptionTexts[0].color.a > 0)
		{
			tempColor = descriptionTexts[0].color;
			tempColor.a -= Time.deltaTime * fadeSpeed;

			foreach (Text element in descriptionTexts)
			{
				element.color = tempColor;	
			}
			yield return null;
		}
		SetTextOpacityTo (0f);

		yield return null;
	}

	void SetTextOpacityTo(float alpha)
	{
		tempColor.a = alpha;
		foreach (Text element in descriptionTexts)
		{
			element.color = tempColor;	
		}
	}

	void ExcludeInfoText()
	{
		int infoIndex = 0;
		foreach (Text element in descriptionTexts)
		{
			if (element.CompareTag("Item Info"))
			{
				infoIndex = descriptionTexts.IndexOf (element);
			}
		}
		descriptionTexts.RemoveAt (infoIndex);
	}

}
