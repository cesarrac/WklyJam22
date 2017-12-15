using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_Manager : MonoBehaviour {
	public static Loot_Manager instance {get; protected set;}
	ObjectPool pool;
	public ItemPrototype lootPrototype;

	void Awake(){
		instance = this;
	}
	void Start(){
		pool = ObjectPool.instance;
	}
	public void DropLoot(Vector2 point){
		int totalToDrop = Random.Range(1, 10);
		for(int i=0; i<totalToDrop; i++){
			Vector2 offset =  point + new Vector2(Random.Range(0, i), Random.Range(0, i));
			if (offset.y > 7){
				offset.y = 7;
			}
			if (offset.y < 4.5f){
				offset.y = 4.5f;
			}
			GameObject itemGobj = pool.GetObjectForType("Item", true, offset);
			itemGobj.GetComponent<Item_Controller>().Init(ItemDatabase.instance.CreateInstance(lootPrototype));
		}
	
	}
}
