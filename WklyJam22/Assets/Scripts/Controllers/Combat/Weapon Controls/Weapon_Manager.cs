using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Manager : MonoBehaviour {

	public static Weapon_Manager instance {get; protected set;}
	Dictionary<ItemUseType, string> controllersMap;

	void Awake(){
		instance = this;
		Init();
	}

	void Init(){
		controllersMap = new Dictionary<ItemUseType, string>();
		controllersMap.Add(ItemUseType.Gun, "Gun Holder");

	}
	public string GetControllerName(ItemUseType useType){
		if (controllersMap.ContainsKey(useType) == false)
			return null;
		return controllersMap[useType];
	}
}
