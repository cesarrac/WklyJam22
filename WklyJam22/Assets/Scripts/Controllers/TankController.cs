using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public Tank myTank {get; protected set;}
	SpriteRenderer tank_spriteR;
	Attack_Controller attack_Controller;
	CombatController combat_Controller;
	void OnEnable(){
		tank_spriteR = GetComponent<SpriteRenderer>();
		attack_Controller = GetComponent<Attack_Controller>();
		combat_Controller = GetComponent<CombatController>();
	}
	public void Init(TankPrototype tankPrototype, Item weapon = null){
		myTank = Tank.CreateInstance(tankPrototype);
		Debug.Log(myTank.name + " copied and initialized with weapon: " + myTank.weapon.name	);
		tank_spriteR.sprite = myTank.sprite;
		combat_Controller.SetHealth(myTank.hitPoints);
		if (weapon != null){
			myTank.SwapWeapon(weapon);
			myTank.weapon.RegisterUser(this.gameObject);
			attack_Controller.SetWeapon(myTank.weapon);
			combat_Controller.SetDamage(myTank.weapon.stats[0]);
			return;
		}
		myTank.weapon.RegisterUser(this.gameObject);
		attack_Controller.SetWeapon(myTank.weapon);
		combat_Controller.SetDamage(myTank.weapon.stats[0]);
	}
	public void SwapWeapon(Item newWeapon){
		// WHAT TO DO WITH OLD WEAPON? Send to an inventory?
		// right now it will just be dumped
		Inventory.instance.AddItem(myTank.weapon);

		myTank.SwapWeapon(newWeapon);
		myTank.weapon.RegisterUser(this.gameObject);
		attack_Controller.SetWeapon(myTank.weapon);

		combat_Controller.SetDamage(myTank.weapon.stats[0]);
		Debug.Log("Weapon swapped to " + newWeapon.name);
	}
	 // TEST:
    public void Attack(CombatController userCmbtCont, CombatController targetCmbtCont){
        // Instantiate projectile
        
    }
}
