using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAttack_Controller : AIAttack_Controller {

	float attackRate = 1.5f;
	Follower_CombatController combatController;
	void Awake(){
		combatController = GetComponent<Follower_CombatController>();
	}
	public override void GetTarget(){

		GameObject[] enemies = Enemy_Manager.instance.GetEnemies();
        target = enemies[Random.Range(0, enemies.Length)];

		base.GetTarget();
	}
    public void StartAttackCycle(){
		GetTarget();
		StopCoroutine("WaitAttackRate");
        if (Combat_Manager.instance.inCombat == false)
            return;
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
		if (target.GetComponent<CombatController>() == null){
			Debug.LogError(target.name + " does not have a combat controller!");
			return;
		}
		combatController.DoDamage(target.GetComponent<CombatController>());
	}
}

// FOLLOWERS WAIT UNTIL THEY ARE NOT MOVING IN ORDER TO ATTACK