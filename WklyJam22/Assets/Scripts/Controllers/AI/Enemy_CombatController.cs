using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CombatController : CombatController {
	
	Stat damage;
	public HealthController health {get; protected set;}
	public void Init(){
		damage = new Stat("Damage", 2, 4);
		health = new HealthController(50);
	}
	public override void ReceiveDamage(CombatController attacker, int damage){
		health.ReceiveDamage(damage);
		base.ReceiveDamage(attacker, damage);
	}
	public override void DoDamage(CombatController target){
		int curDamage = damage.GetValue();
		target.ReceiveDamage(this, curDamage);
	}
	
}
