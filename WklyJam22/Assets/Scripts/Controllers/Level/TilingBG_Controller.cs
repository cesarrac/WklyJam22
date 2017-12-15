using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingBG_Controller : MonoBehaviour {

	[Header("Total world tiles this BG takes up in width")]
	public int tileWorldWidth = 1;
	int lastLeft, lastRight, maxLeft, maxRight;
	public int offset = 4;
	int totalTilesActive = 0;
	public string backgroundID = "Cloud BG Tile";
	ObjectPool pool;
	List<GameObject> active_tiles;
	int areaWidth = 1;
	int lastLeftX, lastRightX;
	public void Init(){
		active_tiles = new List<GameObject>();
		areaWidth = Area_Controller.instance.Current.Width;
		pool = ObjectPool.instance;
		maxLeft = Mathf.CeilToInt((transform.position.x - Camera.main.orthographicSize)-offset);
		maxRight = Mathf.FloorToInt((transform.position.x + Camera.main.orthographicSize)+offset);
		Camera_Controller.instance.onBoundsChanged += OnCameraBoundsChanged;
	}
		void OnCameraBoundsChanged(int left, int right){
	//	Debug.Log("Cam bounds at left " + left + " right " + right);

		if (left < lastLeft){
			// going left
			if (left > 0 && left <= maxLeft + offset){

				maxRight = right + offset;
				PoolRight();
				SpawnLeft();
			}
			
		}else if (right > lastRight){
			// going right
			if (right >= maxRight - offset){
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

		lastLeftX = startX;
		endX = Mathf.Clamp(endX, 1, areaWidth);
		int tilesRequired = ((endX - startX) / tileWorldWidth);
		if (tilesRequired <= 0)
			return;
		int tilesSpawned = 0;
		for(int i = 0; i <= tilesRequired; i++){
			GameObject bgTile = pool.GetObjectForType(backgroundID, true, new Vector2(startX, transform.position.y));
			bgTile.transform.SetParent(this.transform);

			active_tiles.Add(bgTile);
			startX += tileWorldWidth;
			tilesSpawned += 1;
		}
		lastRightX = endX;
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
	void OnDisable(){
		if (Camera_Controller.instance != null)
			Camera_Controller.instance.onBoundsChanged -= OnCameraBoundsChanged;
	}
/* 	void OnCamBoundsChanged(int left, int right){
		if (right > lastRight){
			
			// difference b/w this x and right camera
			int tilesRequired = (right - left) - totalTilesActive;

			if (tilesRequired >  totalTilesActive){
				SpawnRight(Mathf.FloorToInt(tilesRequired));
			}
		}
		lastRight = right; 
	}

	void SpawnRight(int tilesMissing){
		SpawnBGTiles((totalTilesActive * tileWorldWidth) + tileWorldWidth / 2, tilesMissing);
	}
	
	void SpawnBGTiles(int startX, int totalNeeded){
		Debug.Log("SpawnBGTiles needed: " + totalNeeded);
		if (totalNeeded == 0)
			return;
		for(int x = 0; x < totalNeeded; x++){
			Vector2 spawnPos = new Vector2(startX, transform.position.y);
			GameObject newBG = pool.GetObjectForType(backgroundID, true, spawnPos);
			newBG.transform.SetParent(this.transform);
			totalTilesActive += tileWorldWidth;
			startX += tileWorldWidth;
		}
	} */
}
