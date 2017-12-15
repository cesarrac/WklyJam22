using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour {

	public static Enemy_Manager instance {get; protected set;}
	public List<GameObject>enemyGobjs;
	int[] spawnXPositions;
	public delegate void OnEnemyChanged();
	public event OnEnemyChanged onEnemySpawned, onEnemyDied;
	EnemyPrototype[] prototypes;
	public float  timeBetweenSpawns = 30;
	float timer;
	bool waitingToReset;
	void Awake(){
		instance = this;
		enemyGobjs = new List<GameObject>();
		waitingToReset = false;
	}
	void Start(){
		Camera_Controller.instance.onBoundsChanged += CheckCamBoundsForEnemies;
		prototypes = Resources.LoadAll<EnemyPrototype>("Enemy Prototypes/");

		spawnXPositions = SpawnMap_Manager.instance.GetSpawnPositionsOf(SpawnType.Enemy);
	}
	void Update(){
		if (enemyGobjs.Count == 0 && waitingToReset == true){
			if (timer >= timeBetweenSpawns){
				timer = 0;
				waitingToReset = false;
				spawnXPositions = SpawnMap_Manager.instance.GetSpawnPositionsOf(SpawnType.Enemy);
				Debug.Log("ENEMIES RESET!");
			}else{
				timer += Time.deltaTime;
			}
		}
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
		// Which enemy??
		EnemyPrototype enemy = prototypes[Random.Range(0, prototypes.Length)];
		Item enemyWpn = null;
		if (enemy.weaponToUse != null){
			enemyWpn = ItemDatabase.instance.CreateInstance(enemy.weaponToUse); // override tank wpn
		}
		GameObject enemyGobj = ObjectPool.instance.GetObjectForType("Enemy", true, position);
		enemyGobj.GetComponent<EnemyBrain_Controller>().Init();
		enemyGobj.GetComponent<TankController>().Init(enemy.tankToUse, enemyWpn);
		enemyGobj.GetComponent<Enemy_CombatController>().health.onHPZero += (hp) => OnEnemyHPZero(hp, enemyGobj);
		enemyGobj.GetComponent<Enemy_MoveController>().Init(enemy.waitToMove, enemy.moveDuration, enemy.tankToUse.speed);
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
		// Drop some random loot
		Loot_Manager.instance.DropLoot(enemy.transform.position);
		ObjectPool.instance.PoolObject(enemy);

		// END THE COMBAT SEQUENCE
		if (enemyGobjs.Count <= 0){
			if (onEnemyDied != null){
				onEnemyDied();
			}
		}

		waitingToReset = true;
	}
	public void SetMovementClamps(int left, int right){
		foreach(GameObject enemy in enemyGobjs){
			enemy.GetComponent<Enemy_MoveController>().SetMovementClamps(left, right);
		}
	}
	public void PoolAll(){
		List<GameObject>toPool = enemyGobjs;
		foreach(GameObject enemy in toPool){
			ObjectPool.instance.PoolObject(enemy);
			enemyGobjs.Remove(enemy);
		}
		toPool.Clear();
	}
}
