using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Selector : Selector {

	LeaderAttackController leaderAttackController;

	void Start(){
		leaderAttackController = Squad_Manager.instance.leader.GetComponent<LeaderAttackController>();
	}
	public override void Select(){
		leaderAttackController.SetTarget(this.gameObject);
		leaderAttackController.StartAttackCycle();
	}
	
}
