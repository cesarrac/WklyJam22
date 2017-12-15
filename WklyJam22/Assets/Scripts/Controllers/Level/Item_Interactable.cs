using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interactable : Interactable {

	Item_Controller item_Controller;

	void Awake(){
		item_Controller = GetComponent<Item_Controller>();
	}
	public override void Interact(){
		base.Interact();
		item_Controller.PickUp();
	}
}
