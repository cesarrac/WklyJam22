using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Controller : Attack_Controller {
	float attackRate = 2.5f;
	Enemy_CombatController combatController;
	void Awake(){
		combatController = GetComponent<Enemy_CombatController>();
	}
	
	public override void SetWeapon(Item newWeapon){
		base.SetWeapon(newWeapon);

		weapon = newWeapon;
		if (transform.childCount > 0){
			weaponHolder.transform.SetParent(null);
			ObjectPool.instance.PoolObject(weaponHolder);
		}
	
		string newControllerName = Weapon_Manager.instance.GetControllerName(weapon.itemUseType);
		weaponHolder = ObjectPool.instance.GetObjectForType(newControllerName, true, this.transform.position);
		if (weaponHolder != null){
			weaponHolder.name = weapon.name;
			weaponHolder.transform.SetParent(this.transform);
			weaponHolder.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
			curWpnController = weaponHolder.GetComponent<Weapon_Controller>();
		}
	}
	public override void GetTarget(){

		// Get Leader or Follower?
		target = Squad_Manager.instance.GetLeader();
		// Rotate weapon Holder to point at target
		float z = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
		weaponHolder.transform.eulerAngles = new Vector3(0, 0, z);
		base.GetTarget();
	}

	public void StartAttackCycle(){
		GetTarget();
		StopCoroutine("WaitAttackRate");
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
		if (curWpnController == null)
			return;
		base.Attack();
		if (target.GetComponent<CombatController>() == null){
			Debug.LogError(target.name + " does not have a combat controller!");
			return;
		}
		Vector3 direction = target.transform.position - transform.position;
		curWpnController.Attack(origin: weaponHolder.transform.position + direction.normalized, 
								direction: direction,
								destination: target.transform.position, 
								usrCombatControl: combatController, 
								targetCmbtControl: target.GetComponent<CombatController>());
	}

	void OnDisable(){
		StopAllCoroutines();
	}
}
