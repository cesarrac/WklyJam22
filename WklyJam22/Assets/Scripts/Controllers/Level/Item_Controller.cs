using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Controller : MonoBehaviour {

	Item myItem;
	SpriteRenderer spriteRenderer;
	void Awake(){
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	public void Init(Item item){
		myItem = item;
		spriteRenderer.sprite = item.sprite;
	}
	public void PickUp(){
		if (Inventory.instance.AddItem(myItem) == true){
			myItem = null;
			spriteRenderer.sprite = null;
			ObjectPool.instance.PoolObject(this.gameObject);
		}
	}
}
