using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile_Control : Projectile_Controller {
	
	public override void Init(Vector3 direction, Vector2 _destination, CombatController combatCont, CombatController targetCmbtCont){
		base.Init(direction, _destination, combatCont, targetCmbtCont);
	}

	private void Update(){

		transform.position += travelDirection * speed * timeManager.deltaTime;
		if (CheckDistance() == true){
			HitTarget();
		}
	}
	public override bool CheckDistance()
	{
		return base.CheckDistance();
	}
	public override void HitTarget(){
		base.HitTarget();
	}
}
