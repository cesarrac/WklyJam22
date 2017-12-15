using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruiterWork_Controller : NPCWork_Controller {

	public TankPrototype[] available_creations;
	
	public override void InitTasks(){
		base.InitTasks();
		// Add available tasks
		foreach(TankPrototype proto in available_creations){
			available_tasks.Add(new NPCTask("Recruit " + proto.name, proto.timeToCreate,"Machine Part",proto.cost, () => Recruit(proto)));
		}
	}
	void Recruit(TankPrototype tankPrototype){
		Debug.Log("Creating instance of " + tankPrototype.name);
		// spawn a follower
		Squad_Manager.instance.AddNewFollower(tankPrototype);
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
