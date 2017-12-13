using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour {

	ItemDatabase itemDatabase;
	public ItemPrototype testItem;
	void Awake(){
		itemDatabase = new ItemDatabase();
	}
}
