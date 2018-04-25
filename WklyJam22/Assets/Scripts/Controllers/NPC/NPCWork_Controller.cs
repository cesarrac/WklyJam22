using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCWork_Controller : MonoBehaviour {

	public 	Queue<NPCTask> active_tasks;
	public List<NPCTask> available_tasks;
	NPCTask curTask;
	float duration;
	bool atWork = false;
	public TimeManager timeManager;

	NPCProfession curProfession;

	public void Initialize(NPCProfession profession){
		curProfession = profession;
		curProfession.Init(this);
	}
	public void InitTasks(){
		active_tasks = new Queue<NPCTask>();
		available_tasks = new List<NPCTask>();
		timeManager = TimeManager.instance;
	}
	void Update(){
		if (curTask != null && atWork == true){
			if (IsTaskComplete() == true){
				DoTask();
			}
		}
	}
	public bool CanAddTask(NPCTask task){
		if (curProfession.CanWork() == false)
			return false;

		if (active_tasks.Count >= 4)
		 	return false;
		active_tasks.Enqueue(task);
		Debug.Log("Task added to queue: " + task.description);
		return true;
	}
	public void SetNextTask(){
		if (active_tasks.Count <= 0)
			return;
		if (atWork == true)
			return;
		curTask = active_tasks.Dequeue();
		curTask.timeLeft = 0;
		duration = curTask.duration;
		atWork = true;
		Debug.Log("Starting task " + curTask.description);
	}
	public bool IsTaskComplete(){
		if (duration <= 0)
			return true;

		if (curTask.timeLeft >= duration){
			return true;
		}
		else{
			curTask.timeLeft += timeManager.deltaTime;
		}
		return false;
	}
	public void DoTask(){
		Debug.Log("Working on " + curTask.description);
		if (curTask == null){
			atWork = false;
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
		SetNextTask();
	}
}

public class NPCTask{
	public string description;
	public float duration;
	public Action taskAction;
	public string itemRequired;
	public int costOfTask;
	public float timeLeft;
	public NPCTask(string desc, float _duration, string requiredItemName, int cost, Action action){
		description = desc;
		duration = _duration;
		taskAction = action;
		costOfTask = cost;
		timeLeft = 0;
		itemRequired = requiredItemName;
	}
}