using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/New Enemy")]
public class EnemyPrototype : ScriptableObject {
	public new string name = "new enemy";
	public float waitToMove = 1;
	public float moveDuration = 10;
	public TankPrototype tankToUse = null;
	public ItemPrototype weaponToUse = null;
	
}
