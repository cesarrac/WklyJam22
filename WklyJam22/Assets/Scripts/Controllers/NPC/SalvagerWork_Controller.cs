using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvagerWork_Controller : NPCWork_Controller {

	public ItemPrototype machinePartPrototype;
	InventoryUI inventoryUI;
	Item curSalvageItem;
	void Start(){
		inventoryUI = InventoryUI.instance;
	}
	public override void InitTasks(){
		base.InitTasks();
		// Add available tasks
		available_tasks.Add(new NPCTask("Drag Item to Salvage", 10,"Machine Part",0, () =>SalvageDraggedItem(curSalvageItem)));
	}
	public override bool CanAddTask(NPCTask task){
		if (inventoryUI.curDragItem == null)
			return false;
		if (inventoryUI.curDragItem.myItem == null)
			return false;
		if (inventoryUI.curDragItem.myItem.itemType == ItemType.Resource)
			return false;
		curSalvageItem = inventoryUI.curDragItem.myItem;
		if (base.CanAddTask(task) == false)
			return false;
		inventoryUI.curDragItem.EndDrop();

		return true;
	}
	void SalvageDraggedItem(Item itemToSalvage){
		int salvageReturn = Random.Range(1,itemToSalvage.costToCreate + 1);
		// add parts to inventory
		Inventory.instance.AddItem(ItemDatabase.instance.CreateInstance(machinePartPrototype), salvageReturn);
		Debug.Log("Salvaged " + itemToSalvage.name + " and got " + salvageReturn + " parts!");

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
