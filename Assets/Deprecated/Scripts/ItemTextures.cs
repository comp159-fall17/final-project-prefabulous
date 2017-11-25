using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextures : MonoBehaviour {

	public List<Texture> textures = new List<Texture>();
	public static List<Texture> Textures;
	static Dictionary<string, Texture> TextureDictionary = new Dictionary<string, Texture>();

	// Use this for initialization
	void Start () {
		Textures = new List<Texture> (textures);

		TextureDictionary.Add ("Sunflower Seed", Textures[0]);
		TextureDictionary.Add ("Poppy Seed", Textures[1]);

	}

	public static bool TextureExists(string name)
	{
		return TextureDictionary.ContainsKey (name);
	}
	
	public static Texture GetTextureFromName(string name)
	{
			return TextureDictionary [name];
	}
}
