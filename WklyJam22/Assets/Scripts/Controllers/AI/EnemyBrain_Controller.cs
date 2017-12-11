using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain_Controller : MonoBehaviour {

	// Decides what to do  and contains any dialogue and means to trigger it
	EnemyAttack_Controller attack_Controller;
	
	void OnEnable(){
		attack_Controller = GetComponent<EnemyAttack_Controller>();
	}
	public void Init(){
		attack_Controller.StartAttackCycle();
	}

}
