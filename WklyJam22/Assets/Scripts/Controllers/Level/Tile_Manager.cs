using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Manager : MonoBehaviour {
	public static Tile_Manager instance {get; protected set;}
	List<GameObject> active_tiles;
	Area cur_area;
	ObjectPool pool;
	SpriteManager spriteMgr;
	GameObject tileHolder;
	int offset = 2;
	int areaWidth = 1;
	int maxLeft = 16, maxRight = 18;
	int lastLeft, lastRight;
	int lastRightX, lastLeftX;
	void Awake(){
		instance = this;
	}
	public void Init(Area curArea, Vector2 startPoint){
		active_tiles = new List<GameObject>();
		cur_area = curArea;
		areaWidth = cur_area.Width;
		tileHolder = new GameObject();
		tileHolder.name = "Tiles";
		pool = ObjectPool.instance;
		spriteMgr = SpriteManager.instance;
		//Camera_Controller.instance.onBoundsChanged += OnCameraBoundsChanged;
		maxLeft = Mathf.CeilToInt((startPoint.x - Camera.main.orthographicSize)-offset);
		maxRight = Mathf.FloorToInt((startPoint.x + Camera.main.orthographicSize)+offset);
		SpawnTiles(0, areaWidth);
		
	}
	void OnCameraBoundsChanged(int left, int right){
	//	Debug.Log("Cam bounds at left " + left + " right " + right);

		if (left < lastLeft){
			// going left
			if (left > 0 && left < maxLeft + offset){

				maxRight = right + offset;
				PoolRight();
				SpawnLeft();
			}
			
		}else if (right > lastRight){
			// going right
			if (right > maxRight - offset){
				// spawn next right section
				maxLeft = left - offset;
				PoolLeft();
				SpawnRight();
			}
		}
		lastLeft = left;
		lastRight = right; 
	}
	void SpawnLeft(){
		if (maxLeft <= 0)
			return;
		int lastMaxLeft = maxLeft;
		maxLeft -= maxLeft;
		SpawnTiles(maxLeft, lastMaxLeft + offset);
	}
	void SpawnRight(){
		if (maxRight >= areaWidth)
			return;
		maxRight += maxRight;
		SpawnTiles(lastRightX, maxRight + offset);
	}
	void SpawnTiles(int startX, int endX){
		startX = Mathf.Clamp(startX, 0, areaWidth);
		endX = Mathf.Clamp(endX, 1, areaWidth);
		for(int x = startX; x < endX; x++){
			for(int y = 0; y < cur_area.Height; y++){
				Tile tile = cur_area.GetTile(x, y);
				if (tile == null)
					continue;
				GameObject tileGObj = pool.GetObjectForType("Tile", true, new Vector2(x, y));
				tileGObj.transform.SetParent(tileHolder.transform);
				SpriteRenderer sr = tileGObj.GetComponentInChildren<SpriteRenderer>();
				if (tile.spriteID != string.Empty){
					sr.sprite = spriteMgr.GetSprite(tile.spriteID);
				}else{
					sr.sprite = spriteMgr.GetSprite(tile.tileType.ToString());
					tile.SetSpriteID(sr.sprite.name);
				}
				active_tiles.Add(tileGObj);
			}
		}
		lastRightX = endX;
		lastLeftX = startX;
	}
	void PoolLeft(){
		Debug.Log("PoolLeft");
		PoolTiles(0, maxLeft);
	}
	void PoolRight(){
		Debug.Log("PoolRight");
		PoolTiles(maxRight, areaWidth);
	}
	void PoolTiles(int startX, int endX){
		if (endX <= 0){
			return;
		}
		if (active_tiles.Count <= 0){
			return;
		}
		List<GameObject> toRemove = new List<GameObject>();
		foreach(GameObject tile in active_tiles){
			if (tile.transform.position.x >= startX &&
				tile.transform.position.x <= endX){
					toRemove.Add(tile);
				}
		}
		foreach(GameObject tile in toRemove){
			active_tiles.Remove(tile);
			tile.transform.SetParent(null);
			pool.PoolObject(tile);
		}
		toRemove.Clear();
	}
	
	public void SpawnBackgrounds(){
		SpawnClouds();
		SpawnFarRuins(); 
	}
	void SpawnClouds(){
		GameObject clouds = pool.GetObjectForType("Clouds Background", true, new Vector2(10, 8.5f));
		clouds.GetComponent<TilingBG_Controller>().Init();
		clouds.GetComponent<Parallax_Controller>().Init(9f);
	}
	void SpawnFarRuins(){
		GameObject farRuins = pool.GetObjectForType("Far Background", true, new Vector2(10, 8f));
		farRuins.GetComponent<TilingBG_Controller>().Init();
		farRuins.GetComponent<Parallax_Controller>().Init(8f);
	}
}
