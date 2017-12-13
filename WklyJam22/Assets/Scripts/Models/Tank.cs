using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank{

    public string name {get; protected set;}
	public float speed {get; protected set;}
	public Sprite sprite {get; protected set;}
	public RuntimeAnimatorController animatorController {get; protected set;}
	public Item weapon {get; protected set;}
    public int hitPoints {get; protected set;}

    protected Tank(TankPrototype b){
        name = b.name;
        speed = b.speed;
        sprite = b.sprite;
        animatorController = b.animatorController;
        weapon = ItemDatabase.instance.CreateInstance(b.startWeapon);
        hitPoints = b.hitPoints;
    }
    public static Tank CreateInstance(TankPrototype prototype){
        return new Tank(prototype);
    }
    public void SwapWeapon(Item newWeapon){
        if (newWeapon == null)
			return;
		if (newWeapon.itemType != ItemType.Weapon)
			return;
        weapon = newWeapon;
    }
}