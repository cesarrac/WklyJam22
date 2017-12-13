using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class ItemPrototype : ScriptableObject{
    new public string name = "New Item";
    public Sprite sprite = null;
	public ItemType itemType;
    public ItemUseType itemUseType = ItemUseType.None;
	public int stackCount = 50;
    public Stat[] baseStats;
}