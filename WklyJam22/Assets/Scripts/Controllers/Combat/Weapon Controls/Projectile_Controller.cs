using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour {

	public float speed = 4;
	public Vector3 travelDirection = Vector2.zero;
	//public Animator anim;
	public CombatController combat_controller, targetCombatControl;
	public Vector2 destination;
	float curCount;
	public TimeManager timeManager;
	void OnEnable(){
		//anim = GetComponentInChildren<Animator>();
	}
	// Use this for initialization
	public virtual void Init(Vector3 direction, Vector2 _destination, CombatController combatCont, CombatController targetCmbtCont){
		combat_controller = combatCont;
		targetCombatControl = targetCmbtCont;
		destination = _destination;
		travelDirection = direction;
		Debug.Log("Shot projectile towards " + direction + " stopping at " + destination);
		timeManager = TimeManager.instance;
	}
	public virtual bool CheckDistance(){
		if (Vector2.Distance(transform.position, destination) < 1f){
			return true;
		}
		return false;
	}

	public virtual void HitTarget(){
			travelDirection = Vector2.zero;
				// play explosion
			//anim.SetTrigger("hit");
			combat_controller.DoDamage(targetCombatControl);
			ObjectPool.instance.PoolObject(this.gameObject);
	}

/* 	private void Update(){

		transform.position += travelDirection * speed * Time.deltaTime;
	} */


/* 	void OnTriggerEnter2D(Collider2D coll){
		if (coll.GetComponentInParent<CombatController>() != null && combat_controller != null){
			travelDirection = Vector2.zero;
				// play explosion
			//anim.SetTrigger("hit");
			combat_controller.DoDamage(coll.GetComponentInParent<CombatController>());
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<WaitToPoolController>().ResetTo(0.5f);
		}
	}
	void OnDisable(){
		
		GetComponent<BoxCollider2D>().enabled = true;
	} */
}
