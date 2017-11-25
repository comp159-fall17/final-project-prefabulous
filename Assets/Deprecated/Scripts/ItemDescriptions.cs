using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptions: MonoBehaviour {

	static Dictionary<string, string[]> ItemDictionary = new Dictionary<string, string[]>();

	// Use this for initialization
	void Start () {

		ItemDictionary.Add ("Watering Can", new string[] {"Watering Can", "Equip", "E"});
		ItemDictionary.Add ("Planter", new string[] {"Planter", "Plant Seed", "E"});
		ItemDictionary.Add ("Cart", new string[] { "Cart", "Send to Market", "", "" });

		ItemDictionary.Add ("Sunflower Seed", new string[] {"Sunflower Seed", "Take Seed", "E", "sunflower"});
		ItemDictionary.Add ("Sunflower", new string[] {"Sunflower", "Enjoy Flower", "E", "sunflower"});

		ItemDictionary.Add ("Poppy Seed", new string[] {"Poppy Seed", "Take Seed", "E", "poppy"});
		ItemDictionary.Add ("Poppy", new string[] {"Poppy", "Enjoy Flower", "E", "poppy"});


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ChangeEntry(string name, string description, string action, string actionKey	) 
	{
		if (ItemDictionary.ContainsKey(name)) {

			string d, a, aK;
			if (description == "Same")	d = GetDescription (name);
			else d = description; 
			if (action == "Same")		a = GetAction (name);
			else a = action; 
			if (actionKey == "Same")	aK = GetStrippedActionKey(name);
			else aK = actionKey; 

			ItemDictionary [name] = new string[] { d, a, aK };

		} else {
			Debug.Log ("Could not change entry in ItemDictionary");
		}
	}

	public static string GetDescription(string key)
	{
		if (ItemDictionary.ContainsKey (key))
			return ItemDictionary [key][0];
		else
			return "";
	}

	public static string GetAction(string key)
	{
		if (ItemDictionary.ContainsKey (key))
			return ItemDictionary [key][1];
		else
			return "";
	}

	public static string GetActionKey(string key)
	{
		if (ItemDictionary.ContainsKey (key))
			return "[" + ItemDictionary [key][2] + "]";
		else
			return "";
	}

	public static string GetStrippedActionKey(string key)
	{
		if (ItemDictionary.ContainsKey (key))
			return ItemDictionary [key][2];
		else
			return "";
	}

	public static string GetImageName(string key)
	{
		if (ItemDictionary.ContainsKey (key))
			return ItemDictionary [key][3];
		else
			return "";
	}
	public static bool ItemHasAction(string key)
	{
		return ItemDictionary.ContainsKey (key);
	}

}
