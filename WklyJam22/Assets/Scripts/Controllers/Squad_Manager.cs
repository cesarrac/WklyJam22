using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad_Manager : MonoBehaviour {

	public static Squad_Manager instance {get;protected set;}
	FollowerManager followerManager;
	public GameObject leader;
	void Awake(){
		instance = this;
		followerManager = GetComponentInChildren<FollowerManager>();
	}
	public GameObject GetLeader(){
		return leader;
	}
	public GameObject[] GetFollowers(){
		List<GameObject>followers = new List<GameObject>();
		foreach(FollowerMoveControl follower in followerManager.followers){
			followers.Add(follower.gameObject);
		}
		return followers.ToArray();
	}
}
