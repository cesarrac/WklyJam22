using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_Waypoint : Selector {

	FollowerMoveControl[] followers; // followers following this waypoint

	public void SetFollowers(FollowerMoveControl[] _followers){
		followers = _followers;
	}
	public override void Select(){
		base.Select();

		if (followers != null && followers.Length > 0){
			for(int i = 0; i < followers.Length; i++){
				followers[i].CancelWaypoint();
			}
		}
		ObjectPool.instance.PoolObject(this.gameObject);
	}
	
}
