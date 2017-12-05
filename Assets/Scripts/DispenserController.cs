using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DispenserController : VRTK_SnapDropZone {
    //[Header("Seed Spawning")]
    //[Tooltip("The prefab for the seed that this dispenser will contain")]
    //public GameObject seedPrefab;
    //[Tooltip("The point where the seed will spawn")]
    //public GameObject spawnPoint;
    //[Tooltip("The amount the seed size is increased by while inside the dispenser")]
    //public Vector3 seedScalar = new Vector3(10, 10, 10);

    [Header("Market")]
    [Tooltip("The amount of money it costs to buy this seed")]
    public int sellPrice;
    [Tooltip("The amount of money refuneded if the seed is returned")]
    [Range(0f, 1f)]
    public float refundRatio = 1f;

    //GameObject currentSeed;
    //private bool grabbed;

    // Use this for initialization
 //   void Start () {
	//	SpawnSeed();
	//}

	// Update is called once per frame
	//protected override void Update () {
 //       base.Update();
 //       if (!grabbed) {
 //           grabbed = SeedWasGrabbed();
 //           if (grabbed)
 //           {
 //               SeedPicked();
 //               PiggyBankController.Instance.Spend(sellPrice);
 //           }
 //       }
 //       if (SeedWasReleased()) {
 //           SeedDropped();
 //           SpawnSeed();
 //       }
	//}

    //bool SeedWasGrabbed() {
    //    return currentSeed.transform.parent != gameObject.transform;
    //}

    //bool SeedWasReleased()
    //{
    //    if (grabbed)
    //    {
    //        return currentSeed.transform.parent == gameObject.transform;
    //    }
    //    return false;
    //}

    //void SeedPicked() {
    //    currentSeed.transform.localScale = seedPrefab.transform.localScale;
    //}

    //void SeedDropped() {
    //    currentSeed.GetComponent<Rigidbody>().useGravity = true;
    //    currentSeed.transform.parent = null;

    //}

    //void SpawnSeed() {
    //    grabbed = false;
    //    currentSeed = Instantiate(seedPrefab) as GameObject;
    //    currentSeed.transform.parent = gameObject.transform;
    //    currentSeed.transform.localScale = Vector3.Scale(currentSeed.transform.localScale,
    //                                                     seedScalar);
    //    currentSeed.transform.position = spawnPoint.transform.position;
    //    currentSeed.GetComponent<Rigidbody>().useGravity = false;
    //    currentSeed.name = seedPrefab.name;
    //}
}
