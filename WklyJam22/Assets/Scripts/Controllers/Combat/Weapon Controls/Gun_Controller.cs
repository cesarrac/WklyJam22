using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Controller : Weapon_Controller {

	ObjectPool pool;

	public override void Attack(Vector2 origin, Vector3 direction, Vector2 destination, CombatController usrCombatControl, CombatController targetCmbtControl){
		if (pool == null)
			pool = ObjectPool.instance;
		
		// DO FX on origin and then do damage

		// OR shoot projectile
		
		GameObject projectile = pool.GetObjectForType("Gun Projectile", true, origin);
		projectile.GetComponent<GunProjectile_Control>().Init(direction,destination, usrCombatControl, targetCmbtControl); 
	}
}
