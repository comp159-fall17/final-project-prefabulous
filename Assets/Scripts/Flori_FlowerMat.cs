using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_FlowerMat : MonoBehaviour {

	public static Flori_FlowerMat Instance;

	// Use this for initialization
	void Start () {

		if (Instance == null)	
		{
			Instance = this;	
		}
		else if (Instance == this)	
		{
			Destroy(gameObject);	
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Flower"))
		{
			return;
		}

		other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		other.transform.SetParent (transform);
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Flower"))
		{
			return;
		}

		other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		other.transform.SetParent (GameObject.Find("Flowers").transform);
	}

	public void FreezePositionOfFlowersOnMat()
	{
		foreach(Transform child in transform)
		{
			child.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}
	}

	public void FreezeRotationOfFlowersOnMat()
	{
		foreach(Transform child in transform)
		{
			child.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	public void ClearConstraintsOfFlowersOnMat()
	{
		foreach(Transform child in transform)
		{
			child.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}

	public bool ContainsFlower(GameObject flowerToCheck)
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject == flowerToCheck)
			{
				return true;
			}
		}
		return false;
	}

}
