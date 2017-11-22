using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			WateringCan.PlayerIsHolding = false;
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}
}
