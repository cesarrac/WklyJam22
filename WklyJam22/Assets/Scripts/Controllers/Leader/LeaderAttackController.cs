using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderAttackController : Attack_Controller {

	Leader_CombatController combatController;
	Vector2 weaponOffset = new Vector2(0.18f, -0.02f);
	void Awake(){
		combatController = GetComponent<Leader_CombatController>();
	}
	public override void SetWeapon(Item newWeapon){
		base.SetWeapon(newWeapon);

		weapon = newWeapon;
		GameObject oldWpnHolder = GetComponentInChildren<Weapon_Controller>().gameObject;
		if (oldWpnHolder != null){
			oldWpnHolder.transform.SetParent(null);
			ObjectPool.instance.PoolObject(oldWpnHolder);
		}
		
		string newControllerName = Weapon_Manager.instance.GetControllerName(weapon.itemUseType);
		Vector2 weaponPosition = (Vector2)transform.position + weaponOffset;
		weaponHolder = ObjectPool.instance.GetObjectForType(newControllerName, true, weaponPosition);
		if (weaponHolder != null){
			weaponHolder.name = weapon.name;
			weaponHolder.transform.SetParent(this.transform);
			weaponHolder.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
			curWpnController = weaponHolder.GetComponent<Weapon_Controller>();
		}
		attackRate = weapon.stats[1].GetValue();
		accuracy = weapon.stats[2].GetValue();
		if (attackCountdown == null){
			attackCountdown = new CountdownHelper(attackRate);
		}
	}
	public void StartAttackCycle(){
		GetTarget();
		canAttack = true;
		/* StopCoroutine("WaitAttackRate");
		StartCoroutine("WaitAttackRate"); */
	}
	void Update(){
		if (canAttack == true && target != null){
			attackCountdown.UpdateCountdown();
			if (attackCountdown.elapsedPercent >= 1){
				canAttack = false;
				Attack();
				StartAttackCycle();
			}
		}
	}
	public override void Attack(){
		if (curWpnController == null)
			return;
		if (base.CheckForMiss() == false)
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
}
