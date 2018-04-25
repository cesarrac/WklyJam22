using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Tank", menuName = "Tanks/New Tank", order= 2)]
public class TankPrototype : ScriptableObject {
	new public string name = "New Tank";
	public float speed = 1;
	public int hitPoints = 25;
	public Sprite sprite = null;
	public RuntimeAnimatorController animatorController = null;
	[Header("MUST be Item of Weapon type:")]
	public ItemPrototype startWeapon;
	public int cost = 10;

	public float timeToCreate = 1f;
	
}
