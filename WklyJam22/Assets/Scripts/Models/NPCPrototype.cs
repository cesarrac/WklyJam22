using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/New NPC")]
public class NPCPrototype : ScriptableObject {

	public new string name = "New NPC";
	public NPCTYpe npcType = NPCTYpe.Blacksmith;
	public Sprite sprite = null;
	public RuntimeAnimatorController animatorController = null;
}
