using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour {

	public static FollowerManager instance {get; protected set;}
	public List<FollowerMoveControl> followers {get; protected set;}
	FollowerMoveControl selectedFollower;

	void OnEnable(){
		instance = this;
		followers = new List<FollowerMoveControl>();
	}

	public void AddFollower(GameObject newFollower){
		followers.Add(newFollower.GetComponent<FollowerMoveControl>());
	}
	public void SetFollowers(){
		if (transform.childCount <= 0){
			followers.Clear();
		}
		for(int i = 0; i < transform.childCount; i++){
			followers.Add(transform.GetChild(i).gameObject.GetComponent<FollowerMoveControl>());
		}
	}

	public void SetWayPoint(GameObject waypoint){
		Transform point = waypoint.transform;
		Selector_Waypoint wSelector = waypoint.GetComponent<Selector_Waypoint>();

		if (selectedFollower != null){
			selectedFollower.SetWaypoint(point);
			wSelector.SetFollowers(new FollowerMoveControl[]{selectedFollower});
			DeselectFollower();
			return;
		}
		for(int i = 0; i < followers.Count; i++){
			followers[i].SetWaypoint(point);
		}
		wSelector.SetFollowers(followers.ToArray());
	}
	public void CancelWaypoint(){
		for(int i = 0; i < followers.Count; i++){
			followers[i].CancelWaypoint();
		}
	}
	public void SelectFollower(FollowerMoveControl follower){
		selectedFollower = follower;
	}
	public void DeselectFollower(){
		selectedFollower = null;
	}
	public void PoolAll(){
		if (followers.Count <= 0)
			return;
		foreach(FollowerMoveControl follower in followers){
			ObjectPool.instance.PoolObject(follower.gameObject);

		}
		followers.Clear();
	}
}
