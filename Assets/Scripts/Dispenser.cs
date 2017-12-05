using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Dispenser : VRTK_SnapDropZone {
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

    public override void OnObjectUnsnappedFromDropZone(SnapDropZoneEventArgs e)
    {
        base.OnObjectUnsnappedFromDropZone(e);
        PiggyBank.Instance.Spend(sellPrice);
        GetComponent<AudioSource>().Play();
    }
}
