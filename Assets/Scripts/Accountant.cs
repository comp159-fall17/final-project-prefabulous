using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accountant : MonoBehaviour {

	public Text coinCount;
	public static Text CoinCount;
	public static int Balance = 0;

	// Use this for initialization
	void Start () {
		CoinCount = coinCount;
	}
	
	public static void AddCoin(string flowerName)
	{
		if (FlowerDictionary.FlowerIsInHandbook (flowerName))
			Accountant.Balance += FlowerDictionary.GetFlowerWorthFromName (flowerName);
		else
			FlowerDictionary.GetFlowerWorthFromName ("Sunflower");
		
		CoinCount.text = Accountant.GetBalance().ToString ();
	}

	public static int GetBalance()
	{
		return Accountant.Balance;
	}
}
