using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Controller : AIAttack_Controller {
	float attackRate = 2.5f;
	Enemy_CombatController combatController;
	void Awake(){
		combatController = GetComponent<Enemy_CombatController>();
	}
	public override void GetTarget(){

		// Get Leader or Follower?
		target = Squad_Manager.instance.GetLeader();

		base.GetTarget();
	}

	public void StartAttackCycle(){
		GetTarget();
		StopCoroutine("WaitAttackRate");
		StartCoroutine("WaitAttackRate");
	}
	IEnumerator WaitAttackRate(){
		while(true){
			yield return new WaitForSeconds(attackRate);
			Attack();
			StartAttackCycle();
			yield break;
		}
	}

	public override void Attack(){
		base.Attack();
		combatController.DoDamage(target.GetComponent<CombatController>());
	}

	void OnDisable(){
		StopAllCoroutines();
	}
}
