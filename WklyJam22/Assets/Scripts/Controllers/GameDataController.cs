﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour {
	public static GameDataController instance {get; protected set;}
	ItemDatabase itemDatabase;
	void Awake(){
		
		itemDatabase = new ItemDatabase();
	}
	
}
