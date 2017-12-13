using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader_CombatController : CombatController {

	float minDistToAbsorbDmg = 3;
	
	public override void ReceiveDamage(CombatController attacker, int damage){
		if (health.HitPoints <= 0){
			return;
		}
		int totalDamage = damage;

		// Check for followers around leader that can absorb damage
		int squadTotal = Squad_Manager.instance.GetFollowers().Length + 1;
		if (squadTotal > 1){
			totalDamage = damage / squadTotal;
			GameObject[] followers = Squad_Manager.instance.GetFollowers();
			foreach(GameObject follower in followers){
				if (Vector2.Distance(transform.position, follower.transform.position) <= minDistToAbsorbDmg){
					follower.GetComponent<CombatController>().ReceiveDamage(attacker, totalDamage);
					Debug.Log(totalDamage + " damage ABSORBED by " + follower.gameObject.name);
				}
			}
		}
		// Take damage
		health.ReceiveDamage(totalDamage);
		base.ReceiveDamage(attacker, totalDamage);

	}
	public override void DoDamage(CombatController target){
		target.ReceiveDamage(this, damage.GetValue());
	}
	
}
