using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_DyeBowl : MonoBehaviour {

	public Color dyeColor;
	public float dyeingDuration = 1f;

	MeshRenderer seedMesh;

	// Use this for intialization
	void Start() {

		// need to set this correctly to "Dye" GameObject mesh color
		dyeColor = gameObject.transform.parent.GetComponentInChildren<MeshRenderer> ().material.color;

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
		}

		DyeSeed (other.gameObject);
	}

	void DyeSeed(GameObject seed)
	{
		if (seedMesh.material.color != dyeColor)
		{
			seedMesh.material.color = Color.Lerp (seedMesh.material.color, dyeColor, Time.deltaTime / dyeingDuration);
			dyeingDuration -= Time.deltaTime;
			if (seedMesh.material.color == dyeColor)
			{
				dyeingDuration = 1f;
			}
		}


	}

}
