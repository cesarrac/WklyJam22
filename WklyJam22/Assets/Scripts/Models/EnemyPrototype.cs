using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/New Enemy", order= 1)]
public class EnemyPrototype : ScriptableObject {
	new public string name = "New Enemy";
	public float waitToMove = 1;
	public float moveDuration = 10;
	public TankPrototype tankToUse;
	public ItemPrototype weaponToUse;
	
}
