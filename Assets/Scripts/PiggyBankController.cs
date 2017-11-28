using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.GrabAttachMechanics;

public class PiggyBankController : VRTK_ChildOfControllerGrabAttach {
    public GameObject moneyLabel;
    public static PiggyBankController instance;

    [Tooltip("The amount of money the user starts with")]
    [Range(0, 10000)]
    public int startingMoney;
    [Tooltip("Speed at which the text fades in and out")]
    [Range(0.01f, 2f)]
    public float fadeSpeed = 0.75f;

    private int money = 0;
    private Text moneyText;
    Color tempColor;

    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
            UpdateLabel();
        }
    }

    // Use this for initialization
    void Start () {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        moneyText = moneyLabel.GetComponent<Text>();
        Money = startingMoney;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called when the Piggy Bank is initially grabbed by the user
    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint) {
        bool result = base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
        GetComponent<AudioSource>().Play();
        StartCoroutine("FadeTextIn");
        return result;
    }

    // Called when the Piggy Bank is initially dropped by the user
    public override void StopGrab(bool applyGrabbingObjectVelocity) {
        base.StopGrab(applyGrabbingObjectVelocity);
        StartCoroutine("FadeTextOut");
    }

    // Updates the label to display the current balance
    void UpdateLabel() {
        moneyLabel.GetComponent<Text>().text = "$" + String.Format("{0:n}", Money);
    }

    // Borrowed from ItemDescriptionFader.cs
    IEnumerator FadeTextIn()
    {
        while (moneyText.color.a < 1)
        {
            tempColor = moneyText.color;
            tempColor.a += Time.deltaTime * fadeSpeed;

            moneyText.color = tempColor;
            yield return null;
        }
        SetTextOpacityTo(1f);

        yield return null;
    }

    // Borrowed from ItemDescriptionFader.cs
    IEnumerator FadeTextOut()
    {
        while (moneyText.color.a > 0)
        {
            tempColor = moneyText.color;
            tempColor.a -= Time.deltaTime * fadeSpeed;

            moneyText.color = tempColor;
            yield return null;
        }
        SetTextOpacityTo(0f);

        yield return null;
    }

    // Borrowed from ItemDescriptionFader.cs
    void SetTextOpacityTo(float alpha)
    {
        tempColor.a = alpha;
        moneyText.color = tempColor;
    }

    // Reduces the balance by an amount
    void Spend(int amount) {
        Money -= amount;
    }

    // Increases the balance by an amount
    void Earn(int amount) {
        Money += amount;
    }
}
