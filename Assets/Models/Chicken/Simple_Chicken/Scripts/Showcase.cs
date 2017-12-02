using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showcase : MonoBehaviour {

    public Animator animator;
    public Scrollbar rotator;

    private string lastPlayed = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(transform.rotation.x, 360 * rotator.value, transform.rotation.z);
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void StopLoops() {
        animator.SetBool("isWalking", false);
        animator.SetBool("isFlying", false);
    }

    public void PlayAnimation(string idle) {

        if (idle == lastPlayed) {
            return;
        }
        StopLoops();
        animator.SetTrigger(idle);

        lastPlayed = idle;
    }

    public void PlayLoop(string loop) {
        StopLoops();
        animator.SetBool(loop, true);
    }


}
