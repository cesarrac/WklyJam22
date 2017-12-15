using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Resource, Weapon, Utility}
public enum ItemQuality {Terrible, Defective, Functional, Worthy, Majestic}
public enum ItemUseType {None, Gun, Cannon, Part}
public class Item  {
	public string name {get; protected set;}
	public ItemType itemType {get; protected set;}
	public ItemQuality itemQuality {get; protected set;}
	public int stackCount {get; protected set;}
	public ItemUseType itemUseType {get; protected set;}
	public Stat[] stats {get; protected set;}
	public Sprite sprite {get; protected set;}
	public GameObject user;
	public int costToCreate {get; protected set;}
	protected Item(ItemPrototype b, ItemQuality quality, Stat[] newStats){
		this.name = b.name;
		this.itemType = b.itemType;
		this.stackCount = b.stackCount;
		this.itemQuality = quality;
		this.stats = newStats;
		this.sprite = b.sprite;
		this.itemUseType = b.itemUseType;
		this.costToCreate = b.cost;
	}
	public static Item CreateInstance(ItemPrototype prototype, ItemQuality quality, Stat[] newStats){
		return new Item(prototype, quality, newStats);
	}
	public void RegisterUser(GameObject newUser){
		user = newUser;
	}
	/* public void UseItem(){
		if (useAction != null){
			useAction.UseItem(this);
		}
	} */
	
}
