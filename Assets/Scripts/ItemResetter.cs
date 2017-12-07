using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResetter : MonoBehaviour
{

	[Tooltip("Time after which this object will reset after touching the ground.")]
	public float resetTime = 30f;

	[Header("Direct Position Reset")]
	[Tooltip("Override the respawn position and use the overridePosition variable instead.")]
	public bool overrideLocation = false;
	[Tooltip("Location in global coordinates for this object to respawn at if overrideLocation is selected.")]
	public Vector3 overridePosition = Vector3.one;

	[Header("GameObject Position Reset")]
	[Tooltip("Select overrideLocation and this (true) to respawn directly above a GameObject's position in the scene.")]
	public bool useGameObjectForRespawnLocation = false;
	[Tooltip("The GameObject which this object will spawn above if both overrideLocation and useGameObjectForRespawnLocation are selected.")]
	public GameObject gameObjectForRespawn;
	[Tooltip("Distance above the selected GameObject for this object to respawn at.")]
	public float dropHeight = 1f;

	Vector3 startingPosition;
	Vector3 startingRotation;
	float resetCounter = 0f;

	// Use this for initialization
	void Start()
	{

		startingPosition = transform.position;
		startingRotation = transform.eulerAngles;
		if (name.Contains("Seed"))
		{
			gameObjectForRespawn = GameObject.Find ("Seed Bowl");
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Ground"))
		{
			return;
		}

		resetCounter += Time.deltaTime;
		if (resetCounter >= resetTime)
		{
			if (!overrideLocation)
			{
				transform.position = startingPosition;
			}
			else
			{
				if (!useGameObjectForRespawnLocation)
				{
					transform.position = overridePosition;
				}
				else
				{
					Vector3 newPosition = gameObjectForRespawn.transform.position;
					newPosition.y += dropHeight;
					transform.position = newPosition;
				}
			}
			transform.eulerAngles = startingRotation;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Ground"))
		{
			return;
		}

		resetCounter = 0f;
	}

}