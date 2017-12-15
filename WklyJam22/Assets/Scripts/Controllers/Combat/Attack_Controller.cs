using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour {

	public float attackRate = 1.5f;
	public float accuracy = 50;
	public GameObject target;
	public GameObject weaponHolder;
	public Item weapon;
	public Weapon_Controller curWpnController;
	public CountdownHelper attackCountdown;
	public bool canAttack;
	public virtual void SetWeapon(Item newWeapon){
		if (newWeapon.itemType != ItemType.Weapon){
			return;
		}
		
	}

	public virtual void GetTarget(){
		if (target != null){
			//Debug.Log(this.gameObject.name +"'s Target assigned to " + target.name);
		}else{
			//Debug.Log(this.gameObject.name + "No target found!");
		}
	}
	public virtual bool CheckForMiss(){
		if (Random.Range(1, 100) <= accuracy){
			return true;
		}
		// do MISS FX on target
		UI_Manager.instance.DoDamageFX(target.transform.position);
		return false;
	}
	public virtual void Attack(){
		//Debug.Log(this.gameObject.name + " attacks!");
	}
}
