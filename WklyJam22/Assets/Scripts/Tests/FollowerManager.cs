using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour {

	public static FollowerManager instance {get; protected set;}
	public FollowerMoveControl[] followers {get; protected set;}

	void OnEnable(){
		instance = this;
	}

	void Awake(){
		followers = new FollowerMoveControl[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			followers[i] = transform.GetChild(i).gameObject.GetComponent<FollowerMoveControl>();
		}
	}

	public void SetWayPoint(Transform point){
		for(int i = 0; i < followers.Length; i++){
			followers[i].SetWaypoint(point);
		}
	}
	public void CancelWaypoint(){
		for(int i = 0; i < followers.Length; i++){
			followers[i].CancelWaypoint();
		}
	}
}
