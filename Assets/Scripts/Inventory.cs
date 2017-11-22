using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public static Inventory instance = null;
	public GameObject player;
	public Transform inventoryParent;
	public static Transform InventoryParent;
	public GameObject inventoryBar;
	public GameObject[] highlighters;
	public RawImage[] icons;
	
	static Dictionary<string, GameObject> InventoryDictionary = new Dictionary<string, GameObject> ();

	Color rawColor;
	int lastIndex;

	public static int coinCount = 0;

	// Use this for initialization
	void Start () {
		
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		
		InventoryParent = inventoryParent;
		rawColor = Color.white;
		rawColor.a = 225f;

	}
	
	// Update is called once per frame
	void Update () {

		if (InventoryKeyPressed ()) {
			ChangeInventorySlotTo (Input.inputString);
		}


	}

	bool InventoryKeyPressed()
	{
		return (Input.inputString == "1" || Input.inputString == "2" || Input.inputString == "3" || Input.inputString == "4" || Input.inputString == "5" || Input.inputString == "6" || Input.inputString == "7");
	}

	void ChangeInventorySlotTo(string input)
	{
		int newSlot;
		int.TryParse (input, out newSlot);
		if (GetCurrentInventorySlot() != 10) highlighters [GetCurrentInventorySlot ()].SetActive (false);
		highlighters [newSlot-1].SetActive (true);

	}

	public int GetCurrentInventorySlot()
	{
		for (int i = 0; i < highlighters.Length; i++) {
			if (highlighters [i].activeInHierarchy) {
				lastIndex = i;
				return i;
			}
		}
		return 10;
	}

	void SetSlotIcon(string itemName)
	{
		foreach (RawImage raw in inventoryBar.GetComponentsInChildren<RawImage>()) {
			if (raw.name == "Icon" && raw.texture == null) {
				if (ItemTextures.TextureExists(itemName)) {
					raw.texture = ItemTextures.GetTextureFromName (itemName);
					raw.color = rawColor;
				}
				break;
			}
		}
	}

	void ClearHighlightedTexture()
	{
		icons [lastIndex].texture = null;
		icons [lastIndex].color = Color.clear;
		highlighters [lastIndex].SetActive (false);
	}

	public static void AddToInventory(GameObject newItem)
	{
		InventoryDictionary.Add (newItem.name, newItem);
		instance.ChangeInventorySlotTo (InventoryDictionary.Count.ToString());
		instance.SetSlotIcon (newItem.name);
		newItem.transform.SetParent (InventoryParent);
		newItem.SetActive (false);
	}

	public static bool ItemIsInInventory(string itemName)
	{
		return InventoryDictionary.ContainsKey (itemName);
	}

	public static GameObject GetItemFromInventory(string itemName)
	{
		instance.ClearHighlightedTexture ();
		GameObject item = InventoryDictionary [itemName];
		InventoryDictionary.Remove (itemName);
		return item;
	}

	public static string GetHighlightedItemName()
	{
		string[] temp = new string[InventoryDictionary.Count];
		InventoryDictionary.Keys.CopyTo(temp, 0);
		int current = instance.GetCurrentInventorySlot ();
		if (current < temp.Length)
			return temp [current];
		else
			return "Slot Empty";
	}

}
