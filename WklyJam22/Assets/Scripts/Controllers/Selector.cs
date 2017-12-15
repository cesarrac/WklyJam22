using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public virtual void Select(){
		Debug.Log(this.gameObject + " has been selected!");
	}
	public virtual void DropItem(ItemDrag_Controller dragItem){
		
	}
}
