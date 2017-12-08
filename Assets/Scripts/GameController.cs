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
	public Vector2 treeFadeLimits = new Vector2(0.05f, 2f);
	[Tooltip("The delay for trees to disappear after the 'Play Flori' seed is planted")]
	public float disappearTime = 4f;

	[HideInInspector] public bool firstButtonPressed = false;
	[HideInInspector] public bool secondButtonPressed = false;
	[HideInInspector] public bool thirdButtonPressed = false;

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


	public void ManageSceneLoading()
	{
		if (firstButtonPressed && secondButtonPressed) 
		{
			if (thirdButtonPressed)
			{
				switch (SceneManager.GetActiveScene().buildIndex)
				{
				case 0:
					SceneManager.LoadScene (1);
					break;
				case 1:
					SceneManager.LoadScene (0);
					break;
				default:
					Debug.Log ("Scene Manager switch statement defaulting");
					break;
				}
			}
			else
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
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

	public void SetThirdButtonPressed(bool state)
	{
		thirdButtonPressed = state;
	}

	public void OpenIntroPath()
	{
		pathwaysParent.SetActive (true);
		foreach (GameObject tree in introTrees)
		{
			float interval = Random.Range (treeFadeLimits.x, treeFadeLimits.y);
			VAP.SetOpacity (tree, 0f, interval);
		}
		StartCoroutine ("DisableTrees");
	}

	IEnumerator DisableTrees()
	{
		yield return new WaitForSeconds (disappearTime);
		treesParent.SetActive (false);
	}

	public void LeaveTheGarden()
	{
		Application.Quit ();
	}

}
