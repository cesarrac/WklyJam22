using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCProfession : ScriptableObject {
	public string pName = "New Profession";
	public NPCTYpe npcType = NPCTYpe.Blacksmith;

	public abstract void Init(NPCWork_Controller work_Controller);
	public abstract bool CanWork();
}
