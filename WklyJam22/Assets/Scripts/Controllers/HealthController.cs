using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController  {

	int starting_hitpoints;
	int pHitpoints;
	int hitPoints {
		get{return pHitpoints;}
		set{pHitpoints = Mathf.Clamp(value, 0, starting_hitpoints);}
	}
	public int HitPoints{get {return hitPoints;}}

	public delegate void OnHPChanged(int hp);
	public event OnHPChanged onHPChanged;
	public HealthController(int hPAtStart){
		starting_hitpoints = hPAtStart;
		hitPoints = starting_hitpoints;
		Debug.Log("Health-control initialized with " + hPAtStart + " hp");
	}
	public void ReceiveDamage(int dmg){
		hitPoints -= dmg;
		if (onHPChanged != null){
			onHPChanged(hitPoints);
		}
	}
}
