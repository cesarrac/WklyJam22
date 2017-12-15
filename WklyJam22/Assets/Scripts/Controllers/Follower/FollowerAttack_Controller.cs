using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAttack_Controller : Attack_Controller {


	Follower_CombatController combatController;
	void Awake(){
		combatController = GetComponent<Follower_CombatController>();
	}
	public override void SetWeapon(Item newWeapon){
		base.SetWeapon(newWeapon);
		GameObject oldWpnHolder = null;
		if (GetComponentInChildren<Weapon_Controller>() != null)
			oldWpnHolder = GetComponentInChildren<Weapon_Controller>().gameObject;

		if (oldWpnHolder != null){
			oldWpnHolder.transform.SetParent(null);
			oldWpnHolder.name = weapon.itemUseType + " Holder";
			ObjectPool.instance.PoolObject(oldWpnHolder);
		}

		weapon = newWeapon;
		
		
		string newControllerName = Weapon_Manager.instance.GetControllerName(weapon.itemUseType);
		weaponHolder = ObjectPool.instance.GetObjectForType(newControllerName, true, this.transform.position);
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
		weaponHolder.transform.eulerAngles = new Vector3(0, 0, -90);
	}
	public override void GetTarget(){

		GameObject[] enemies = Enemy_Manager.instance.GetEnemies();
        target = enemies[Random.Range(0, enemies.Length)];
		// Rotate weapon Holder to point at target
		float z = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
		weaponHolder.transform.eulerAngles = new Vector3(0, 0, z);
		base.GetTarget();
	}
    public void StartAttackCycle(){
		//StopCoroutine("WaitAttackRate");
        if (Combat_Manager.instance.inCombat == false){
			 weaponHolder.transform.eulerAngles = new Vector3(0, 0, -90);
			 return;
		 }
		GetTarget();
		canAttack = true;
		//StartCoroutine("WaitAttackRate");
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
	/* IEnumerator WaitAttackRate(){
		while(true){
			yield return new WaitForSeconds(attackRate);
            Attack();
			StartAttackCycle();
			yield break;	
		}
	} */
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

// FOLLOWERS WAIT UNTIL THEY ARE NOT MOVING IN ORDER TO ATTACK