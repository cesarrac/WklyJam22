using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_Item : Selector {

	Item_Interactable interactable;
	void Awake(){
		interactable = GetComponent<Item_Interactable>();

	}
	public override void Select(){
		if (interactable == null)
			return;
		if (interactable.CanInteract(Squad_Manager.instance.leader) == true){
			interactable.Interact();
		}
		/* else{
			FollowerManager.instance.SetWayPoint(this.gameObject);
		} */
	}
	
}
