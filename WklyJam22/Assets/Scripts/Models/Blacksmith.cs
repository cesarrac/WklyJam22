using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCProf_Blacksmith", menuName="NPC Professions/Blacksmith")]
public class Blacksmith : NPCProfession {

	public ItemPrototype[] available_creations;
	public override void Init(NPCWork_Controller work_Controller){
			work_Controller.InitTasks();
			foreach(ItemPrototype proto in available_creations){
				work_Controller.available_tasks.Add(
					new NPCTask("Create " + proto.name, proto.timeToCreate,"Machine Part",proto.cost, () => Create(proto)));
			}
	}
	public override bool CanWork(){
		return true;
	}
	public void Create(ItemPrototype itemPrototype){
		Inventory.instance.AddItem(ItemDatabase.instance.CreateInstance(itemPrototype));
	}
}
