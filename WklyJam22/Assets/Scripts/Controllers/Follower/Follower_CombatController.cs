using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_CombatController : CombatController {
	GameObject leader;
	int flankingBonus = 5;
	
	public override void ReceiveDamage(CombatController attacker, int damage){
		if (health.HitPoints <= 0){
			return;
		}
		health.ReceiveDamage(damage);
		base.ReceiveDamage(attacker, damage);
		if (damage > 0){
			FX_Manager.instance.DoFX(FXType.Hit, transform.position);
		}
	}
	public override void DoDamage(CombatController target){
		int dmgModifier = 0;
		// Check if flanking
		if (leader == null){
			leader = Squad_Manager.instance.GetLeader();
		}
		if (transform.position.x > target.transform.position.x){
			if (leader.transform.position.x < target.transform.position.x){
				// Add flanking bonus
				dmgModifier += flankingBonus;
				Debug.Log("adding FLANKING bonus!");
			}
		}else if (transform.position.x < target.transform.position.x){
			if (leader.transform.position.x > target.transform.position.x){
				// Add flanking bonus
				dmgModifier += flankingBonus;
				Debug.Log("adding FLANKING bonus!");
			}
		}

		target.ReceiveDamage(this, damage.GetValue(dmgModifier));
	
	}
}
