using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserController : MonoBehaviour {
    [Tooltip("The prefab for the seed that this dispenser will contain")]
    public GameObject seedPrefab;
    [Tooltip("The point where the seed will spawn")]
    public GameObject spawnPoint;

    GameObject currentSeed;
    private bool grabbed;

    // Use this for initialization
    void Start () {
		SpawnSeed();
	}

	// Update is called once per frame
	void Update () {
        if (!grabbed) {
            grabbed = SeedWasGrabbed();
            SeedPicked();
        }
        if (SeedWasReleased()) {
            Debug.Log("Released");
            SeedDropped();
            SpawnSeed();
        }
	}

    bool SeedWasGrabbed() {
        return currentSeed.transform.parent != gameObject.transform;
    }

    bool SeedWasReleased()
    {
        if (grabbed)
        {
            return currentSeed.transform.parent == gameObject.transform;
        }
        return false;
    }

    void SeedPicked() {
        currentSeed.transform.localScale = seedPrefab.transform.localScale;
    }

    void SeedDropped() {
        currentSeed.GetComponent<Rigidbody>().useGravity = true;
        currentSeed.transform.localScale = seedPrefab.transform.localScale;

    }

    void SpawnSeed() {
        grabbed = false;
        currentSeed = Instantiate(seedPrefab,
                    spawnPoint.transform.position,
                    Quaternion.identity) as GameObject;
        currentSeed.transform.parent = gameObject.transform;
        currentSeed.transform.localScale = Vector3.Scale(currentSeed.transform.localScale,
                                                         new Vector3(4, 4, 4));
        currentSeed.GetComponent<Rigidbody>().useGravity = false;
    }
}
