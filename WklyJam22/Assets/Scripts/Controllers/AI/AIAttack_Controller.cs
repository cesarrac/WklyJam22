using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack_Controller : MonoBehaviour {

	public GameObject target;

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
