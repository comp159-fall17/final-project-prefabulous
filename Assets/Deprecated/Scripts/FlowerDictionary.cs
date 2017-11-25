using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDictionary : MonoBehaviour {

	public Transform flowerParent;
	public static Transform FlowerParent;
	public GameObject[] flowerModels;
	public static GameObject[] FlowerModels;
	public static Dictionary<string, GameObject> FlowerHandbook = new Dictionary<string, GameObject>();
	public static Dictionary<string, float[]> FloweringData = new Dictionary<string, float[]> ();

	// Use this for initialization
	void Start () {
		FlowerModels = flowerModels;
		FlowerParent = flowerParent;

		FlowerHandbook.Add ("Sunflower", FlowerModels[0]);
		FlowerHandbook.Add ("Poppy", FlowerModels[1]);

		FloweringData.Add ("Sunflower", new float[] { 50.0f, 2f });
		FloweringData.Add ("Poppy", new float[] { 30.0f, 6f });
	}

	public static bool FlowerIsInHandbook(string name)
	{
		return FlowerHandbook.ContainsKey (name);
	}

	public static GameObject GetFlowerModelFromName(string name)
	{
		Debug.Log ("giving flower");
		return (GameObject)Instantiate (FlowerHandbook[name]);
	}

	public static float GetFloweringSpeedFromName(string name)
	{
		return FloweringData [name][0];
	}

	public static int GetFlowerWorthFromName(string name)
	{
		return (int)FloweringData [name] [1];
	}
}
