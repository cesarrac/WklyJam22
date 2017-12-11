using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Controller : MonoBehaviour {

	public static Area_Controller instance {get; protected set;} 
	public Area Current {get; protected set;}
	AreaFiller area_filler;
	ObjectPool pool;
	SpriteManager spriteMgr;
	GameObject tileHolder;

	void Awake(){
		instance = this;
	}
	void Start(){
		tileHolder = new GameObject();
		tileHolder.name = "Tiles";
		pool = ObjectPool.instance;
		spriteMgr = SpriteManager.instance;
		GenerateArea();
	}

	void GenerateArea(){
		Current = new Area(24, 10, "test");
		Current.Generate();
		if (area_filler == null)
			area_filler = new AreaFiller();
		area_filler.FillArea(Current);
		SpawnTiles();
	}

	void SpawnTiles(){
		int width = Current.Width;
		int height = Current.Height;
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				Tile tile = Current.GetTile(x, y);
				if (tile == null)
					continue;
				GameObject tileGObj = pool.GetObjectForType("Tile", true, new Vector2(x, y));
				tileGObj.transform.SetParent(tileHolder.transform);
				SpriteRenderer sr = tileGObj.GetComponentInChildren<SpriteRenderer>();
				sr.sprite = spriteMgr.GetSprite(tile.tileType.ToString());
			}
		}
	}
	
}
