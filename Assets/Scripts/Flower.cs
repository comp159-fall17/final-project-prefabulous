using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	public bool canBePicked = false;
	public Vector3 detachedScale;

	// Use this for initialization
	void Start () {
		detachedScale = new Vector3 (10f, 10f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool CanBePicked()
	{
		return canBePicked;
	}

	public void SetCanBePicked(bool maybe)
	{
		canBePicked = maybe;
	}

	public void Pick()
	{
		transform.SetParent (FlowerDictionary.FlowerParent);
		transform.localScale = detachedScale;
		Vector3 flowerPosition = transform.position;
		flowerPosition.y += 0.75f;
		transform.localPosition = flowerPosition;
		transform.gameObject.AddComponent <Holdable>();
		transform.gameObject.AddComponent <Rigidbody>();
		FloriPlayerController.instance.HoldGivenObject (gameObject);
	}

}
