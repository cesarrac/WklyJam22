using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Manager : MonoBehaviour {

	public static Combat_Manager instance {get; protected set;}
	GameObject combatFocus;
	public bool inCombat {get; protected set;}
	void Awake(){
		instance = this;
	}
	void Start(){
		Enemy_Manager.instance.onEnemySpawned += StartCombat;
		Enemy_Manager.instance.onEnemyDied += EndCombat;
	}
	void StartCombat(){
		if (combatFocus == null){
			combatFocus = new GameObject();
			combatFocus.name = "Combat Focus";
			combatFocus.transform.position = new Vector2(Camera.main.transform.position.x - Camera.main.orthographicSize, 4.2f);;
		}
		combatFocus.SetActive(true);
		Camera_Controller.instance.SetTargetAndLock(combatFocus.transform);
		inCombat = true;
		// Clamp character movement
	}
	void EndCombat(){
		combatFocus.SetActive(false);
		Camera_Controller.instance.SetTargetAndLock(Squad_Manager.instance.GetLeader().transform);
		inCombat = false;
		// Un-Clamp character movement
	}
}
