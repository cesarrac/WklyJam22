using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCProf_Recruiter", menuName="NPC Professions/Recruiter")]
public class Recruiter : NPCProfession {

	public TankPrototype[] available_tanks;
	public override void Init(NPCWork_Controller work_Controller){
			work_Controller.InitTasks();
			foreach(TankPrototype proto in available_tanks){
				work_Controller.available_tasks.Add(
				new NPCTask("Recruit " + proto.name, proto.timeToCreate,"Machine Part",proto.cost, () => Recruit(proto)));
			}
	}
	public override bool CanWork(){
		return true;
	}
	public void Recruit(TankPrototype tankPrototype){
		Debug.Log("Creating instance of " + tankPrototype.name);
		// spawn a follower
		Squad_Manager.instance.AddNewFollower(tankPrototype);
	}
}
