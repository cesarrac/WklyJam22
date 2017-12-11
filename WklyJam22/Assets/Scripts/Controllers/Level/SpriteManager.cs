using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	public static SpriteManager instance {get; protected set;}
	Dictionary<string, Sprite> spriteMap;
	public Sprite defaultSprite;
	Area_Controller areaController;
	public Sprite[] floorChoices;
	void Awake(){
		instance = this;
		Init();
	}
	void Init(){
		spriteMap = new Dictionary<string, Sprite>();
		Sprite[] tiles = Resources.LoadAll<Sprite>("Sprites/mockupTiles");
		for(int i = 0; i < tiles.Length; i++){
			spriteMap.Add(tiles[i].name, tiles[i]);
		}
	}
	void Start(){
		//areaController = AreaController.instance;
	}
	public Sprite GetSprite(string name){
		if (name == null)
			return defaultSprite;
		if(name == "Floor" && floorChoices.Length > 0){
			return floorChoices[Random.Range(0, floorChoices.Length)];
		}	
		if (spriteMap.ContainsKey(name) == false){
			Debug.Log("Sprite Manager could not find sprite: " + name);
			return defaultSprite;
		}
		return spriteMap[name];
	}
}
