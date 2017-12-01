using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.GrabAttachMechanics;

public class PiggyBankController : VRTK_ChildOfControllerGrabAttach {
    
	public static PiggyBankController Instance;

    [Tooltip("The amount of money the user starts with")]
    [Range(0, 10000)]
    public int startingMoney;
    [Tooltip("Speed at which the text fades in and out")]
    [Range(0.01f, 2f)]
    public float fadeSpeed = 0.75f;

    Flori_UIData uiData;
    Color tempColor;

	int _money = 0;
    public int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            UpdateLabel();
        }
    }

    // Use this for initialization
    void Start () {
		
        if (Instance == null) 
		{
            Instance = this;
        } 
		else if (Instance != this) 
		{
            Destroy(this);
        }
        uiData = GetComponent<Flori_UIData>();
        money = startingMoney;

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
    void UpdateLabel() 
	{
		uiData.SetItemDescription(String.Format("${0:n}", money));
	}

    /// <summary>
	/// Reduces the balance by an amount.
    /// </summary>
    /// <param name="amount">Amount.</param>
    public void Spend(int amount) 
	{
        money -= amount;
        Debug.Log(_money);
    }

    /// <summary>
	/// Increases the balance by an amount.
    /// </summary>
    /// <param name="amount">Amount.</param>
    public void Earn(int amount) 
	{
        money += amount;
        Debug.Log(_money);
    }
}
