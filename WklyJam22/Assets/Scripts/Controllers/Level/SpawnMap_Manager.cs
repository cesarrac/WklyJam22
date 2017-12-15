using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType {Enemy, NPC, Player}
public class SpawnMap_Manager : MonoBehaviour {
	public static SpawnMap_Manager instance {get; protected set;}
	Dictionary<int, SpawnType> spawnX_Map;
	void Awake(){
		instance = this;
		spawnX_Map = new Dictionary<int, SpawnType>();
		InitEnemies();
		IntitNPCs();
	}
	void IntitNPCs(){
		int[] spawnPositions = new int[]{13, 16, 19};
		for(int i = 0; i < spawnPositions.Length; i++){
			AddToMap(spawnPositions[i], SpawnType.NPC);
		}
	}
	void InitEnemies(){
		int[] spawnPositions = new int[]{32};
		for(int i = 0; i < spawnPositions.Length; i++){
			AddToMap(spawnPositions[i], SpawnType.Enemy);
		}
	}
	void AddToMap(int key, SpawnType value){
		if (spawnX_Map.ContainsKey(key) == true){
			Debug.Log("SpawnMap could not add " + key + " because it already exists in map!");
			return;
		}
		spawnX_Map.Add(key, value);
	}

	public int[] GetSpawnPositionsOf(SpawnType spwnType){
		List<int>spawnPositions = new List<int>();
		foreach(int key in spawnX_Map.Keys){
			if (spawnX_Map[key] == spwnType){
				spawnPositions.Add(key);
			}
		}
		return spawnPositions.ToArray();
	}
}
