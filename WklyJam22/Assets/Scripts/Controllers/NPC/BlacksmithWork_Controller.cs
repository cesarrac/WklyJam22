using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithWork_Controller : NPCWork_Controller {

	public ItemPrototype[] available_creations;
	
	public override void InitTasks(){
		base.InitTasks();
		// Add available tasks
		foreach(ItemPrototype proto in available_creations){
			available_tasks.Add(new NPCTask("Create " + proto.name, proto.timeToCreate,"Machine Part",proto.cost, () => Create(proto)));
		}
	}
	void Create(ItemPrototype itemPrototype){
		Debug.Log("Creating instance of " + itemPrototype.name);
		Inventory.instance.AddItem(ItemDatabase.instance.CreateInstance(itemPrototype));
	}
	void Update(){
		if (curTask != null && atWork == true){
			if (base.IsTaskComplete() == true){
				DoTask();
			}
		}
	
	}
	public override void DoTask(){
		if (curTask == null){
			return;
		}
		if (curTask.taskAction == null){
			Debug.LogError("Could not DO " + curTask.description + " because its action is null!");
			return;
		}
		curTask.taskAction();

		NPC_UIManager.instance.OnTaskDone(GetComponent<NPC_Controller>(), curTask);
		curTask = null;
		atWork = false;
		base.SetNextTask();
	}
}
