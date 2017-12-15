using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPCWork_Controller : MonoBehaviour {

	public 	Queue<NPCTask> active_tasks;
	public List<NPCTask> available_tasks;
	public NPCTask curTask;
	float duration;
	float timer;
	public bool atWork = false;
	public TimeManager timeManager;
	public virtual void InitTasks(){
		active_tasks = new Queue<NPCTask>();
		available_tasks = new List<NPCTask>();
		timeManager = TimeManager.instance;
	}
	public virtual bool CanAddTask(NPCTask task){
		if (active_tasks.Count >= 4)
		 	return false;
		active_tasks.Enqueue(task);
		return true;
		Debug.Log("Task added to queue: " + task.description);
	}
	public virtual void SetNextTask(){
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
	public virtual bool IsTaskComplete(){
		if (duration <= 0)
			return true;

		if (curTask.timeLeft >= duration){
			timer = 0;
			return true;
		}
		else{
			curTask.timeLeft += timeManager.deltaTime;
		}
		return false;
	}
	public virtual void DoTask(){
		Debug.Log("Working on " + curTask.description);
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