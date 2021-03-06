﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_DyeBowl : MonoBehaviour {

	public float dyeingDuration = 5f;
	public GameObject dye;

	Color dyeColor;
	List<MeshRenderer> seedMeshes = new List<MeshRenderer>();

	float dyeCounter;

	// Use this for intialization
	void Start() {

		dyeColor = dye.GetComponent<MeshRenderer>().material.color;
		dyeCounter = dyeingDuration;

	}

	void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Seed"))
		{
			return;
		}
		if (!seedMeshes.Contains(other.GetComponent<MeshRenderer>()))
		{
			seedMeshes.Add (other.GetComponent<MeshRenderer> ());
		}

		foreach (MeshRenderer seedMesh in seedMeshes) 
		{
			DyeSeed (seedMesh);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Seed"))
		{
			return;
		}

		seedMeshes.Remove(other.GetComponent<MeshRenderer>());
	}

	void DyeSeed(MeshRenderer seedMesh)
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
