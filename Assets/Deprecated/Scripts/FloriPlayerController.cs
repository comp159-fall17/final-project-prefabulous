using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloriPlayerController : MonoBehaviour {

	public GameObject player;
	public static float DistanceFromTarget;
	public float toTarget, pickupDistance, breakForce = 200f;
	public Vector3 itemPosition = new Vector3(0f, 0f, 1f);
	public Text itemDescriptionText, itemActionText, itemActionKeyText;
	public bool isHoldingCan = false;
	Inventory inventory;
	public static FloriPlayerController instance = null;

	RaycastHit hit;
	FixedJoint joint;
	GameObject objectHeld;

	Vector3 raycastOrigin;
	bool objectPickedUp = false;
	string itemInfo, itemActionKey;

	// Use this for initialization
	void Start () {

		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

	}

	// Update is called once per frame
	void Update () {

		HandleObjects ();

	}

	void HandleObjects()
	{
		raycastOrigin = transform.position + new Vector3 (0f, 1f, 0f);
		if (Physics.Raycast(raycastOrigin, Camera.main.transform.forward, out hit)) {
			toTarget = hit.distance;
			DistanceFromTarget = toTarget;

			if (toTarget < pickupDistance) {

				DisplayItemInfo (hit.transform.gameObject);

				// retrieve action key associated with object <3
				KeyCode actionKey;
				try { actionKey = (KeyCode)System.Enum.Parse (typeof(KeyCode), itemActionKey); } 
				catch { actionKey = KeyCode.E; }

				if (ItemDescriptions.ItemHasAction(hit.transform.gameObject.name) && Input.GetKeyDown(actionKey)) {


					HandleInventoryItem ();
					HandleWateringCan ();
					HandleHoldable ();
					HandlePlanters ();
					HandleFlowers ();

				}
			}
			else {
				if (itemDescriptionText.text != "") {
					ClearItemText ();
				}
			}
		}
	}

	void HandleHoldable()
	{
		if (joint == null && hit.transform.gameObject.GetComponent<Holdable> () != null && !objectPickedUp) { 
			Debug.Log ("Flower");
			HoldObject ();
		} else if (objectHeld != null) {
			DropObject ();
		}
	}

	void HoldObject()
	{
		joint = gameObject.AddComponent<FixedJoint> () as FixedJoint;
		joint.breakForce = breakForce;
		joint.connectedBody = hit.transform.GetComponent<Rigidbody> ();
		objectHeld = hit.transform.gameObject;
		objectPickedUp = true;
		itemActionText.text = "Drop";
	}

	public void HoldGivenObject(GameObject probablyFlower)
	{
		joint = gameObject.AddComponent<FixedJoint> () as FixedJoint;
		joint.connectedBody = probablyFlower.GetComponent<Rigidbody> ();
		objectHeld = probablyFlower.gameObject;
		objectPickedUp = true;
		itemActionText.text = "Drop";
	}

	void DropObject()
	{
		itemActionText.text = ItemDescriptions.GetAction (objectHeld.name);
		FixedJoint.Destroy (joint);
		objectHeld = null;
		objectPickedUp = false;
	}

	public void ForceDropObject()
	{
		itemActionText.text = "";
		FixedJoint.Destroy (joint);
		objectHeld = null;
		objectPickedUp = false;
	}

	void HandleInventoryItem()
	{
		if (hit.transform.GetComponent<InventoryItem> () != null) {
			Debug.Log ("inside");

			Inventory.AddToInventory (hit.transform.gameObject);

		} else if (hit.transform.GetComponentInParent<InventoryItem>() != null) {

			// don't collect when flower is childed to seed and is blooming
			if (hit.transform.GetComponentInParent<Seed>() != null && !hit.transform.GetComponentInParent<Seed> ().collectable) {

				Debug.Log ("no flower");

			} else {
				Inventory.AddToInventory (hit.transform.parent.gameObject);
			}
		}
	}

	void HandleWateringCan()
	{
		if (hit.transform.gameObject.name == "Watering Can") {
			if (!WateringCan.PlayerIsHolding) WateringCan.EquipWateringCan ();
		}
	}

	void HandlePlanters()
	{
		if (hit.transform.gameObject.name == "Planter") {
			Planter planter;

			if (hit.transform.gameObject.GetComponent<Planter>() == null) planter = hit.transform.gameObject.GetComponentInParent<Planter>();
			else planter = hit.transform.gameObject.GetComponent<Planter>();

			if (Inventory.GetHighlightedItemName ().Contains ("Seed")) {

				planter.PlantCrop (Inventory.GetItemFromInventory (Inventory.GetHighlightedItemName ()));
				ChangeDescriptionForWatering(planter);

			} else {
				if (WateringCan.PlayerIsHolding && planter.hasCrop && !planter.GetSeedInPlanter().flower.GetComponent<Flower>().CanBePicked()) {
					planter.StartGrowingFlower ();
				}
			}

		}
	}

	void HandleFlowers()
	{
		if (hit.transform.CompareTag("Flower") || hit.transform.parent.CompareTag("Flower")) {

			Flower flower;
			if (hit.transform.gameObject.CompareTag("Flower")) flower = hit.transform.gameObject.GetComponent<Flower>();
			else flower = hit.transform.parent.gameObject.GetComponent<Flower>();

			Debug.Log (flower.gameObject.name);

			if (flower.CanBePicked()) 
			{
				GameObject parentSeed = flower.gameObject.transform.parent.gameObject;
				Planter planter = parentSeed.transform.parent.gameObject.GetComponent<Planter>();
				flower.Pick ();
				Destroy (parentSeed);
				planter.RemoveCropFrom ();
			} 
			else 
			{
				DJOsvaldo.PlayClipAt ("cheer");
			}


		}
	}

	void ChangeDescriptionForWatering(Planter planter)
	{
		if (WateringCan.PlayerIsHolding) {
			if (planter.hasCrop && !planter.GetSeedInPlanter().flower != null) {
				if (!planter.GetSeedInPlanter ().flower.GetComponent<Flower> ().CanBePicked ()) {
					ItemDescriptions.ChangeEntry (hit.transform.gameObject.name, "Same", "Water Seed", "Same");
					itemActionText.text = ItemDescriptions.GetAction (hit.transform.gameObject.name);
				}
			}
		}

	}

	public void DisplayItemInfo(GameObject item)
	{
		if (ItemDescriptions.GetDescription(item.name) != itemInfo) {
			itemInfo = ItemDescriptions.GetDescription (item.name);
			//			itemInfo = item.name; // TODO: remove after testing
			itemDescriptionText.text = itemInfo;
			itemActionText.text = ItemDescriptions.GetAction (item.name);
			itemActionKey = ItemDescriptions.GetStrippedActionKey (item.name);
			itemActionKeyText.text = ItemDescriptions.GetActionKey (item.name);
		}
	}

	void ClearItemText()
	{
		itemInfo = "";
		itemDescriptionText.text = "";
		itemActionText.text = "";
		itemActionKeyText.text = "";
	}



}