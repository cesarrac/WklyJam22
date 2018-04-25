using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/New NPC", order= 4)]
public class NPCPrototype : ScriptableObject {

	public string npcName = "New NPC";
	//public NPCTYpe npcType = NPCTYpe.Blacksmith;
	public NPCProfession profession;
	public Sprite sprite = null;
	public RuntimeAnimatorController animatorController = null;
}
