using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour {

	public static FollowerManager instance {get; protected set;}
	public List<FollowerMoveControl> followers {get; protected set;}

	void OnEnable(){
		instance = this;
		followers = new List<FollowerMoveControl>();
	}

	public void AddFollower(GameObject newFollower){
		followers.Add(newFollower.GetComponent<FollowerMoveControl>());
	}
	public void SetFollowers(){
		
		for(int i = 0; i < transform.childCount; i++){
			followers.Add(transform.GetChild(i).gameObject.GetComponent<FollowerMoveControl>());
		}
	}

	public void SetWayPoint(Transform point){
		for(int i = 0; i < followers.Count; i++){
			followers[i].SetWaypoint(point);
		}
	}
	public void CancelWaypoint(){
		for(int i = 0; i < followers.Count; i++){
			followers[i].CancelWaypoint();
		}
	}
}
