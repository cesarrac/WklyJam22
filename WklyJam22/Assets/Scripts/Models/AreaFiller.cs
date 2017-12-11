using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFiller  {

	Area curArea;

	public void FillArea(Area area){
		curArea = area;
		if (curArea == null){
			Debug.LogError("AreaFiller received a NULL area! Did Area Controller generate it?");
			return;
		}
		int width = curArea.Width;
		int height = curArea.Height;

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				if(y <= 1){
					curArea.tileGrid[x, y].SetAs(TileType.Bottom);
					continue;
				}
				if (y == 2){
					curArea.tileGrid[x, y].SetAs(TileType.Edge_Bottom);
					continue;
				}
				if (y >= height - 2){
					curArea.tileGrid[x, y].SetAs(TileType.Top);
					continue;
				}
				if (y == height - 3){
					curArea.tileGrid[x, y].SetAs(TileType.Edge_Top);
					continue;
				}
				curArea.tileGrid[x, y].SetAs(TileType.Floor);
			}
		}
	}
}
