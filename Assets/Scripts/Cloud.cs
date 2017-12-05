using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float speedMultiplier = 1f;

	// Use this for initialization
	void Start () {
        bool xDir = UnityEngine.Random.Range(0f, 1f) > 0.5;
        bool zDir = UnityEngine.Random.Range(0f, 1f) > 0.5;
        int xSign = -1;
        if (xDir) {
            xSign = 1;
        }
        int zSign = -1;
        if (zDir)
        {
            zSign = 1;
        }
		GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(0f, 1f) * xSign * speedMultiplier,
                                                       0,
                                                       UnityEngine.Random.Range(0f, 1f) * zSign * speedMultiplier));
	}
}
