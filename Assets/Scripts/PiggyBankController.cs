using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.GrabAttachMechanics;

public class PiggyBankController : VRTK_ChildOfControllerGrabAttach {
    public static PiggyBankController instance;

    [Tooltip("The amount of money the user starts with")]
    [Range(0, 10000)]
    public int startingMoney;
    [Tooltip("Speed at which the text fades in and out")]
    [Range(0.01f, 2f)]
    public float fadeSpeed = 0.75f;

    Flori_UIData uiData;
    int money = 0;
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
        uiData = GetComponent<Flori_UIData>();
        Money = startingMoney;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called when the Piggy Bank is initially grabbed by the user
    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint) {
        bool result = base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
        GetComponent<AudioSource>().Play();
        return result;
    }

    // Called when the Piggy Bank is initially dropped by the user
    public override void StopGrab(bool applyGrabbingObjectVelocity) {
        base.StopGrab(applyGrabbingObjectVelocity);
    }

    // Updates the label to display the current balance
    void UpdateLabel() {
        uiData.SetItemDescription("$" + String.Format("{0:n}", Money));
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
