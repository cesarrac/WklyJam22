using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour {

	public GameObject target;
	public GameObject weaponHolder;
	public Item weapon;
	public Weapon_Controller curWpnController;

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

	public virtual void Attack(){
		//Debug.Log(this.gameObject.name + " attacks!");
	}
}
