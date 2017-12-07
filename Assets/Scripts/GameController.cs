using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


	[HideInInspector] public bool firstButtonPressed = false;
	[HideInInspector] public bool secondButtonPressed = false;

	// Use this for initialization
	void Start() {

		firstButtonPressed = false;
		secondButtonPressed = false;

	}


	public void RestartScene()
	{
		if (firstButtonPressed && secondButtonPressed) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void SetFirstButtonPressed(bool state)
	{
		firstButtonPressed = state;
	}

	public void SetSecondButtonPressed(bool state)
	{
		secondButtonPressed = state;
	}


}
