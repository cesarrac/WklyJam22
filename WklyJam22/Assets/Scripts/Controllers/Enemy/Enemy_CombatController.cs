﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CombatController : CombatController {
	

	public override void ReceiveDamage(CombatController attacker, int damage){
		if (health.HitPoints <= 0){
			return;
		}
		health.ReceiveDamage(damage);
		base.ReceiveDamage(attacker, damage);
	}
	public override void DoDamage(CombatController target){
		int curDamage = damage.GetValue();
		target.ReceiveDamage(this, curDamage);
	}
	
}
