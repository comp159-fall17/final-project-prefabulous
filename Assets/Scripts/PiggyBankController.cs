using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.GrabAttachMechanics;

public class PiggyBankController : VRTK_ChildOfControllerGrabAttach {
    public GameObject moneyLabel;

    [Tooltip("The amount of money the user starts with")]
    [Range(0, 10000)]
    public int startingMoney;
    [Tooltip("Speed at which the text fades in and out")]
    [Range(0.01f, 2f)]
    public float fadeSpeed = 0.75f;

    int money = 0;
    private Text moneyText;
    Color tempColor;

    // Use this for initialization
    void Start () {
        moneyText = moneyLabel.GetComponent<Text>();
        UpdateMoney(startingMoney);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint) {
        bool result = base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
        GetComponent<AudioSource>().Play();
        StartCoroutine("FadeTextIn");
        return result;
    }

    public override void StopGrab(bool applyGrabbingObjectVelocity) {
        base.StopGrab(applyGrabbingObjectVelocity);
        StartCoroutine("FadeTextOut");
    }

    void UpdateMoney(int newValue) {
        money = newValue;
        moneyLabel.GetComponent<Text>().text = "$" + String.Format("{0:n}", money);
    }

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

    void SetTextOpacityTo(float alpha)
    {
        tempColor.a = alpha;
        moneyText.color = tempColor;
    }
}
