using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour {

	public string npcName {get; protected set;}
	//public NPCTYpe npcType {get; protected set;}
	public NPCWork_Controller work_Controller {get; protected set;}
	SpriteRenderer spriteRenderer;
	void OnEnable(){
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		work_Controller = GetComponent<NPCWork_Controller>();
	}
	public void Init(NPCPrototype npcPrototype, NPCProfession profession){
		npcName = npcPrototype.npcName;
		//npcType = npcPrototype.npcType;
		spriteRenderer.sprite = npcPrototype.sprite;
		work_Controller.Initialize(profession);
		//work_Controller.InitTasks();
	}
	public void OnSelected(){
		NPC_UIManager.instance.OnNPCSelected(npcName, spriteRenderer.sprite, this, work_Controller.available_tasks.ToArray(), work_Controller.active_tasks.ToArray());
	}
	public bool CanAddTask(NPCTask task){
		
		/* if (Inventory.instance.ContainsItem(task.itemRequired, task.costOfTask) == false){
			return false;
		} */
		if (work_Controller.CanAddTask(task) == false){
			return false;
		}
		/* if (Inventory.instance.RemoveItem(task.itemRequired, task.costOfTask) == false){
			return false;
		} */
		
		work_Controller.SetNextTask();
	
		return true;
	}
	public void DoAction(){
		Debug.Log(npcName + " is now doing action!");
	}
}

