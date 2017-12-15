using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public CircleCollider2D interact_radius;
	public GameObject user;
	public virtual bool CanInteract(GameObject _user){
		if (interact_radius.OverlapPoint(_user.transform.position)){
			user = _user;
			return true;
		}
		return false;
	}
	public virtual void Interact(){
		Debug.Log(user.name + " is interacting with " + this.gameObject.name);
	}
}
