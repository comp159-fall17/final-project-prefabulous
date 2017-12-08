using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class IntroPlanter : VRTK_SnapDropZone {

	[Header("Watering Can Properties")]
	[Tooltip("Signifies if the planter has a crop planted in it")]
	public bool hasCrop = false;

	Flori_Seed seedInPlanter;

	protected override void SnapObjectToZone (VRTK.VRTK_InteractableObject objectToSnap)
	{
		base.SnapObjectToZone (objectToSnap);
		if (!hasCrop) 
		{
			PlantSeed (objectToSnap);
		}
	}

	public override void OnObjectUnsnappedFromDropZone (SnapDropZoneEventArgs e)
	{
		RemoveCropFrom ();
		base.OnObjectUnsnappedFromDropZone (e);
	}

	/// <summary>
	/// Plants the seed in this Planter instance.
	/// </summary>
	/// <param name="seed">Seed.</param>
	void PlantSeed(VRTK_InteractableObject seed)
	{
		seedInPlanter = seed.GetComponent<Flori_Seed>();
		hasCrop = true;

		if (seed.name == "If You Dare")
		{
			GameController.Instance.LeaveTheGarden ();
		}
		else if (seed.name == "Be Happy")
		{
			LockSeedInPlanter ();
			GameController.Instance.OpenIntroPath ();
		}

	}

	void LockSeedInPlanter()
	{
		if (seedInPlanter != null)
		{
			seedInPlanter.GetComponent<VRTK_InteractableObject> ().isGrabbable = false;
		}
	}

	/// <summary>
	/// Removes the crop from this planter.
	/// </summary>
	public void RemoveCropFrom()
	{
		seedInPlanter = null;
		hasCrop = false;
	}

}
