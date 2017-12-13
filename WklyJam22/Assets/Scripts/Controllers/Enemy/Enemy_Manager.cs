using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour {

	public static Enemy_Manager instance {get; protected set;}
	public List<GameObject>enemyGobjs;
	int[] spawnXPositions;
	public delegate void OnEnemyChanged();
	public event OnEnemyChanged onEnemySpawned, onEnemyDied;
	public TankPrototype enemyTank;
	void Awake(){
		instance = this;
	}
	void Start(){
		Camera_Controller.instance.onBoundsChanged += CheckCamBoundsForEnemies;
		enemyGobjs = new List<GameObject>();
		spawnXPositions = new int[]{16};
	}
	void CheckCamBoundsForEnemies(int leftX, int rightX){
		for(int i = 0; i < spawnXPositions.Length; i++){
			if (spawnXPositions[i] > leftX+3 && spawnXPositions[i] < rightX - 3){
				SpawnEnemy(new Vector2(spawnXPositions[i], 5));
				spawnXPositions[i] = -100;
			}
		}


		// NOTE ALL ENEMIES WILL SPAWN AT THE CENTER WITH Y = 4
		// What tile is at edge right?
	/* 	Area curArea = Area_Controller.instance.Current;
		for(int x = leftX; x < rightX; x++){
		 	if (curArea.tileGrid[x, 4].enemy != null){
				// Spawn enemy
			//	SpawnEnemy(curArea.tileGrid[x, 4].enemy);
			} 
		} */
	}
	void SpawnEnemy(Vector2 position){
		GameObject enemyGobj = ObjectPool.instance.GetObjectForType("Enemy", true, position);
		enemyGobj.GetComponent<EnemyBrain_Controller>().Init();
		enemyGobj.GetComponent<TankController>().Init(enemyTank);
		enemyGobj.GetComponent<Enemy_CombatController>().health.onHPZero += (hp) => OnEnemyHPZero(hp, enemyGobj);
		enemyGobjs.Add(enemyGobj);

		// START THE COMBAT SEQUENCE
		if (onEnemySpawned != null){
			onEnemySpawned();
		}
	}
	public GameObject[] GetEnemies(){
		return enemyGobjs.ToArray();
	}
	void OnEnemyHPZero(int hitPoints, GameObject enemy){
		if (hitPoints > 0){
			return;
		}
		enemy.GetComponent<Enemy_CombatController>().health.onHPZero -= (hp) => OnEnemyHPZero(hp, enemy);
		// Spawn explosion
		FX_Manager.instance.DoFX(FXType.Death, enemy.transform.position);
		OnEnemyDead(enemy);
	}
	public void OnEnemyDead(GameObject enemy){
		enemyGobjs.Remove(enemy);
		ObjectPool.instance.PoolObject(enemy);

		// END THE COMBAT SEQUENCE
		if (enemyGobjs.Count <= 0){
			if (onEnemyDied != null){
				onEnemyDied();
			}
		}
	}
}
