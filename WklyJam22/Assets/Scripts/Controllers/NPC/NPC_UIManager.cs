using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_UIManager : MonoBehaviour {

	public static NPC_UIManager instance {get; protected set;}
	public NPCUI_Controller uiControl;
	NPC_Controller curSelected;
	void Awake(){
		instance = this;
	}
	public void OnNPCSelected(string npcName,Sprite npcSprite, NPC_Controller npcControl,  NPCTask[] available, NPCTask[] active){
		curSelected = npcControl;
		uiControl.gameObject.SetActive(true);
		uiControl.OnNPCChanged(npcName, npcSprite, npcControl, available , active);
	}
	public void OnTaskDone(NPC_Controller npc, NPCTask task){
		if (npc != curSelected)
			return;
		uiControl.RemoveCurrentTask(task);
	}

}
