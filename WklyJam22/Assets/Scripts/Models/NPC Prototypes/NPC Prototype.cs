using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCTYpe {Blacksmith, Trader, Recruiter, Salvager}
[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/New NPC")]
public class NPCPrototype : ScriptableObject {

	public new string name = "Dude";
	public NPCTYpe npcType = NPCTYpe.Blacksmith;
	public Sprite sprite = null;
	public RuntimeAnimatorController animatorController = null;
	
	// ADD DIALOGUE HERE?
}
