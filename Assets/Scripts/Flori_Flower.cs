using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Flower : MonoBehaviour {

	[Header("VRTK Variables")]
	[Tooltip("Signifies if the flower can be picked")]
	public bool canBePicked = false;
	[Tooltip("Signifies if the flower is attached to a seed")]
	public bool isAttached = true;

	[Header("Flower Coloring Data")]
	[Tooltip("Object mesh in flower bloom to be colored.")]
	public List<MeshRenderer> blooms = new List<MeshRenderer>();
	[Tooltip("Object mesh in flower stamen to be colored.")]
	public List<MeshRenderer> stamens = new List<MeshRenderer>();
	[Tooltip("Object mesh of flower stem to be colored.")]
	public List<MeshRenderer> stems = new List<MeshRenderer>();
	[Tooltip("All object meshes in flower leaves to be colored.")]
	public List<MeshRenderer> leaves = new List<MeshRenderer>();

	[Header("Flower Data")]
	[Tooltip("Amount of money this flower sells for at the market")]
	public int flowerWorth = 5;

	Flori_Seed parentSeed;

	// Use this for initialization
	void Start() {

		if (GetComponentInParent < Flori_Seed>() != null) 
		{
			parentSeed = GetComponentInParent<Flori_Seed> ();
		}	

	}

	public void SetCanBePickedTo(bool state)
	{
		canBePicked = state;
	}

	public void Detach()
	{
		isAttached = false;
	}

	public bool CanBePicked()
	{
		return canBePicked;
	}

	public bool IsAttached()
	{
		return isAttached;
	}

	/// <summary>
	/// Gets the parent seed of this flower if any.
	/// </summary>
	/// <returns>The parent seed.</returns>
	public Flori_Seed GetParentSeed()
	{
		return parentSeed;
	}

	public void SetBloomColor(Color bloomColor)
	{
		foreach (MeshRenderer bloom in blooms) 
		{
			bloom.material.color = bloomColor;
		}
	}

	public void SetStamenColor(Color stamenColor)
	{
		foreach (MeshRenderer stamen in stamens)
		{
			stamen.material.color = stamenColor;
		}
	}
		
	public void SetLeavesColor(Color leafColor)
	{
		foreach (MeshRenderer leaf in leaves)
		{
			leaf.material.color = leafColor;
		}
	}

	public void SetStemColor(Color stemColor)
	{
		foreach (MeshRenderer stem in stems) 
		{
			stem.material.color = stemColor;
		}
	}

}
