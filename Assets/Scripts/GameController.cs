using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VAP = VRTK.VRTK_ObjectAppearance;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	[Header("Intro Variables")]
	[Tooltip("The GameObject named 'Intro Trees'.")]
	public GameObject treesParent;
	[Tooltip("The GameObject named 'Intro Pathways'.")]
	public GameObject pathwaysParent;
	[Tooltip("Limits for randomly fading trees in intro.")]
	[Range(0.1f, 5f)]
	public Vector2 treeFadeLimits = new Vector2(0.05f, 0.4f);

	[HideInInspector] public bool firstButtonPressed = false;
	[HideInInspector] public bool secondButtonPressed = false;

	List<GameObject> introTrees = new List<GameObject>();
	List<GameObject> introPathways = new List<GameObject>();

	// Use this for initialization
	void Start() {

		if (Instance == null) 
		{
			Instance = this;
		}
		else if (Instance == this)	
		{
			Destroy(gameObject);	
		}

		firstButtonPressed = false;
		secondButtonPressed = false;

		foreach (Transform child in treesParent.transform)
		{
			introTrees.Add(child.parent.gameObject);
		}
		foreach (Transform child in pathwaysParent.transform)
		{
			introPathways.Add(child.parent.gameObject);
		}

		pathwaysParent.SetActive(false);

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

	public void OpenIntroPath()
	{
		pathwaysParent.SetActive (true);
		foreach (GameObject tree in introTrees)
		{
			float interval = Random.Range (treeFadeLimits.x, treeFadeLimits.y);
			VAP.SetOpacity (tree, 0f, interval);
		}
	}

	public void LeaveTheGarden()
	{
		Application.Quit ();
	}

}
