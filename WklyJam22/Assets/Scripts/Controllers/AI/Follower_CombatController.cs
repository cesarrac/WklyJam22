using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_CombatController : CombatController {
	Stat damage;
	public HealthController health {get; protected set;}
	GameObject leader;
	int flankingBonus = 5;
	void Awake(){
		damage = new Stat("Damage", 2, 4);
		health = new HealthController(25);
	}
	public override void ReceiveDamage(CombatController attacker, int damage){
		health.ReceiveDamage(damage);
		base.ReceiveDamage(attacker, damage);
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
