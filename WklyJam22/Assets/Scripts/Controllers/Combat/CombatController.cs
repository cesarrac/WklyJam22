using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {


	public Stat damage;
	public HealthController health;

	public virtual void SetHealth(int startingHP){
		if (health == null){
			health = new HealthController(startingHP);
		}
	}
	public virtual void SetDamage(Stat weaponDamage){
		damage = weaponDamage;
	}
	public virtual void ReceiveDamage(CombatController attacker, int damage){
		Debug.Log(this.gameObject.name + " receives " + damage);
		
		// Place UI with damage on screen
		UI_Manager.instance.DoDamageFX(this.transform.position, damage);
	}
	public virtual void DoDamage(CombatController target){

	}
}
