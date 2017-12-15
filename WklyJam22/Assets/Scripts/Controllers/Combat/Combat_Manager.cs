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
		int leftClamp = Mathf.FloorToInt(combatFocus.transform.position.x - (Camera.main.orthographicSize * 0.5f));
		int rightClamp = Mathf.CeilToInt(combatFocus.transform.position.x + (Camera.main.orthographicSize * 2) + 2);
		Squad_Manager.instance.SetMovementClamps(leftClamp, rightClamp);
		Enemy_Manager.instance.SetMovementClamps(leftClamp, rightClamp);
	}
	void EndCombat(){
		combatFocus.SetActive(false);
		combatFocus = null;
		Camera_Controller.instance.SetTargetAndLock(Squad_Manager.instance.GetLeader().transform);
		inCombat = false;
		// Un-Clamp character movement
		Squad_Manager.instance.SetMovementClampsToLevel();
	}
}
