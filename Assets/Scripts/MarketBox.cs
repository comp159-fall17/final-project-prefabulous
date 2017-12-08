using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.PhysicsBased;
using UnityEngine.UI;

public class MarketBox : MonoBehaviour {
	
    [HideInInspector]
    public List<GameObject> inCollision;

	[Header("Text Variables")]
	[Tooltip("The text that will display the amount of money a disappeared object is sold for.")]
	public Text moneyAddedText;
	[Tooltip("The increment between both alpha increments and its fade speed.")]
	public float fadeIncrement = 0.01f;
	[Tooltip("How long the text will stay visible without any fading.")]
	public float textVisiblePeriod = 0.1f;

	CanvasRenderer textRenderer;

	// Use this for initialization
	void Start () {
        
		textRenderer = moneyAddedText.gameObject.GetComponent<CanvasRenderer> ();
		textRenderer.SetAlpha (0f);

	}
	
	// Update is called once per frame
	void Update () {
		
        if (inCollision.Count > 0) 
		{
            VanishItems();
        }

	}

    void VanishItems() 
	{
        List<GameObject> toBeDestroyed = new List<GameObject>();
        foreach (GameObject go in inCollision) 
		{
            Sellable worth = go.GetComponent<Sellable>();
            if (worth != null)
            {
                PiggyBank.Instance.Earn(worth.worth);
				moneyAddedText.text = "+$" + worth.worth.ToString();
				StartCoroutine ("FadeMoneyText");
                toBeDestroyed.Add(go);
            }
        }
        foreach (GameObject go in toBeDestroyed) 
		{
            inCollision.Remove(go);
            Destroy(go);
        }
    }

	IEnumerator FadeMoneyText()
	{
		while (textRenderer.GetAlpha() < 1f)
		{
			textRenderer.SetAlpha (textRenderer.GetAlpha() + fadeIncrement);
			yield return new WaitForSeconds (fadeIncrement);
		}
		textRenderer.SetAlpha (1f);

		yield return new WaitForSeconds (textVisiblePeriod);

		while (textRenderer.GetAlpha() > 0f)
		{
			textRenderer.SetAlpha (textRenderer.GetAlpha() - fadeIncrement);
			yield return new WaitForSeconds (fadeIncrement);
		}
		textRenderer.SetAlpha (0f);
		yield return null;
	}
}
