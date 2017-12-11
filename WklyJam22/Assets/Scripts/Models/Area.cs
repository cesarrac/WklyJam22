using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area  {

	public string name;
	public int Width{get;protected set;}
	public int Height{get; protected set;}
	public Tile[,] tileGrid {get; protected set;}
	public Area(int _width, int _height, string _name){
		Width = _width;
		Height = _height;
		name = _name;
	}
	public void Generate(int _width = 0, int _height = 0){
		// Re-set width if params set
		if (_width > 0 && _height > 0){
			Width = _width;
			Height = _height;
		}
	
		InitTileGrid();
	}
	void InitTileGrid(){
		tileGrid = new Tile[Width, Height];
		for(int x = 0; x < Width; x++){
			for(int y = 0; y < Height; y++){
				tileGrid[x, y] = new Tile(x, y, TileType.Empty, new Vector2(x, y));
		 	}
		}
	} 
		public Tile GetTile(int x, int y){
		if (IsInMapBounds(x, y)){
			return tileGrid[x, y];
		}
		return null;
	}
	public Tile GetTile(Vector3 pos)
    {
        int X = Mathf.FloorToInt(pos.x);
        int Y = Mathf.FloorToInt(pos.y);
        if (IsInMapBounds(X, Y) == false)
        {
            return null;
        }

        return tileGrid[X, Y];
    }

    public bool IsInMapBounds(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
            return true;
        return false;
    }
}
