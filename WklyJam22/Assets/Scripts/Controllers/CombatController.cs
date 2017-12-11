using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

	public virtual void ReceiveDamage(CombatController attacker, int damage){
		Debug.Log(this.gameObject.name + " receives " + damage);
	}
	public virtual void DoDamage(CombatController target){

	}
}
