using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NPCProf_Salvager", menuName="NPC Professions/Salvager")]
public class Salvager : NPCProfession {

	public ItemPrototype machinePartPrototype;
	Item curSalvageItem;
	public override void Init(NPCWork_Controller work_Controller){
			
			work_Controller.InitTasks();
			work_Controller.available_tasks.Add(new NPCTask("Drag Item to Salvage", 10,"Machine Part",0, () =>SalvageDraggedItem(curSalvageItem)));
	}
	public override bool CanWork(){
		if (InventoryUI.instance.curDragItem == null)
			return false;
		if (InventoryUI.instance.curDragItem.myItem == null)
			return false;
		if (InventoryUI.instance.curDragItem.myItem.itemType == ItemType.Resource)
			return false;
		curSalvageItem = InventoryUI.instance.curDragItem.myItem;
	
		InventoryUI.instance.curDragItem.EndDrop();

		return true;
	}
	public void SalvageDraggedItem(Item itemToSalvage){
		int salvageReturn = Random.Range(1,itemToSalvage.costToCreate + 1);
		// add parts to inventory
		Inventory.instance.AddItem(ItemDatabase.instance.CreateInstance(machinePartPrototype), salvageReturn);
		Debug.Log("Salvaged " + itemToSalvage.name + " and got " + salvageReturn + " parts!");

	}
}
