using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_DyeBowl : MonoBehaviour {

	public float dyeingDuration = 5f;
	public Material dyeMaterial;

	Color dyeColor;
	MeshRenderer seedMesh;
	Material newMaterial;

	float dyeCounter;

	// Use this for intialization
	void Start() {

		// need to set this correctly to "Dye" GameObject mesh color
		dyeColor = dyeMaterial.color;
		dyeCounter = dyeingDuration;
		newMaterial = new Material(Shader.Find("Standard"));

	}

	void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Seed"))
		{
			return;
		}
		if (seedMesh != other.GetComponent<MeshRenderer>())
		{
			seedMesh = other.GetComponent<MeshRenderer> ();
			seedMesh.material = newMaterial;
		}

		DyeSeed (other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Seed"))
		{
			return;
		}

		seedMesh = null;
		Debug.Log ("Exiting");
	}

	void DyeSeed(GameObject seed)
	{
		if (seedMesh.material.color != dyeColor)
		{
			seedMesh.material.color = Color.Lerp (seedMesh.material.color, dyeColor, Time.deltaTime / dyeCounter);
			dyeCounter -= Time.deltaTime;
			if (seedMesh.material.color == dyeColor)
			{
				dyeCounter = dyeingDuration;
			}
		}


	}

}
