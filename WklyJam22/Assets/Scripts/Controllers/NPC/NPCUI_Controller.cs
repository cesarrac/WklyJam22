using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI_Controller : MonoBehaviour {

	public Text name;
	public Image portrait;
	public GameObject availableTaskParent;
	public Button dismissButton;
	public GameObject activeTasksParent;
	List<GameObject> active_tasks, available_tasks;
	ObjectPool pool;
	NPC_Controller curNPCControl;
	void Awake(){
			active_tasks = new List<GameObject>();
			available_tasks = new List<GameObject>();
	}
	void Start(){
	
		pool = ObjectPool.instance;
		// dismiss can be the same for all
		dismissButton.onClick.AddListener(() => DismissPanel());
	}

	public void OnNPCChanged(string npcName, Sprite npcSprite, NPC_Controller npcControl, NPCTask[] available, NPCTask[] active){
		if (pool == null){
			pool = ObjectPool.instance;
		}
		PoolTasks();
		
		curNPCControl = npcControl;
		name.text = npcName;
		portrait.sprite = npcSprite;
		foreach(NPCTask task in available){
			GameObject taskGbj = pool.GetObjectForType("Task", true, availableTaskParent.transform.position);
			taskGbj.transform.SetParent(availableTaskParent.transform);
			taskGbj.GetComponent<Button>().onClick.RemoveAllListeners();
			taskGbj.GetComponent<Button>().onClick.AddListener(() => AddTask(taskGbj, task));
			taskGbj.GetComponentInChildren<Text>().text = task.description + " " + task.costOfTask;
			available_tasks.Add(taskGbj);
		}
		foreach(NPCTask task in active){
			GameObject taskGbj = pool.GetObjectForType("Task", true, activeTasksParent.transform.position);
			taskGbj.transform.SetParent(activeTasksParent.transform);
			taskGbj.GetComponent<Button>().onClick.RemoveAllListeners();
			taskGbj.GetComponent<Button>().onClick.AddListener(() => CancelTask(taskGbj, task));
			taskGbj.GetComponentInChildren<Text>().text = task.description;
			active_tasks.Add(taskGbj);
		}
	}
	void PoolTasks(){
		if (active_tasks.Count > 0){
			
		
			foreach(GameObject task in active_tasks){
				task.transform.SetParent(null);
				pool.PoolObject(task);
			}
			active_tasks.Clear();
		}

		if (available_tasks.Count <= 0){
			return;
		}
		foreach(GameObject task in available_tasks){
			task.transform.SetParent(null);
			pool.PoolObject(task);
		}
		available_tasks.Clear();
	}
	void AddTask(GameObject taskGobj, NPCTask task){
		if (curNPCControl.CanAddTask(task) == true){
			available_tasks.Remove(taskGobj);
			taskGobj.transform.SetParent(null);
			taskGobj.transform.position = activeTasksParent.transform.position;
			taskGobj.transform.SetParent(activeTasksParent.transform);
			taskGobj.GetComponent<Button>().onClick.RemoveAllListeners();
			taskGobj.GetComponentInChildren<Text>().text = "Working on " + task.description + " " + ((task.duration - task.timeLeft) / 60).ToString("f1") + "m";
			taskGobj.GetComponent<Button>().onClick.AddListener(() => CancelTask(taskGobj, task));
			active_tasks.Add(taskGobj);
		}
	}

	void CancelTask(GameObject taskGobj, NPCTask task){
		Debug.Log("Cancelling task " + task.description);
	}
	public void RemoveCurrentTask(NPCTask task){
		RemoveTask(active_tasks[0], task);
	}
	void RemoveTask(GameObject taskGobj, NPCTask task){
		active_tasks.Remove(taskGobj);
		taskGobj.transform.SetParent(null);
		taskGobj.transform.position = availableTaskParent.transform.position;
		taskGobj.transform.SetParent(availableTaskParent.transform);
		taskGobj.GetComponent<Button>().onClick.RemoveAllListeners();
		taskGobj.GetComponentInChildren<Text>().text = task.description;
		taskGobj.GetComponent<Button>().onClick.AddListener(() => AddTask(taskGobj, task));
		available_tasks.Add(taskGobj);
	}
	void DismissPanel(){
		this.gameObject.SetActive(false);
	}
}
