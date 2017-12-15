using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad_Manager : MonoBehaviour {

	public static Squad_Manager instance {get;protected set;}
	FollowerManager followerManager;
	public GameObject leader {get; protected set;}
	public TankPrototype leaderTank, followerTank;
	Inventory squad_inventory;
	int levelMinX, levelMaxX;
	public int currFollowerCount {
		get{
			if (followerManager == null)
				return 0;
			else
				return followerManager.followers.Count;
		}
	}
	ObjectPool pool;
	void Awake(){
		instance = this;
		followerManager = GetComponentInChildren<FollowerManager>();

		squad_inventory = new Inventory(10);
	}
	public void SpawnSquad(Vector2 leaderPos, int areaWidth){
		if (pool == null)
			pool = ObjectPool.instance;
		// Leader:
		leader = pool.GetObjectForType("Leader", true, leaderPos);
		leader.transform.SetParent(this.transform);
		leader.GetComponent<TankController>().Init(leaderTank);
	
		// TEST spawn follower
		for (int i = 0; i < 2; i++){
			AddNewFollower(followerTank);
		}
		levelMinX = 1;
		levelMaxX = areaWidth - 1;
		SetMovementClampsToLevel();
		/* if (currFollowerCount > 0){
			// Spawn followers
		} */

	}
	public void AddNewFollower(TankPrototype followerTank){
		GameObject follower = pool.GetObjectForType("Follower", true, leader.transform.position + Vector3.left);
		follower.transform.SetParent(followerManager.transform);
		follower.GetComponent<FollowerMoveControl>().Init();
		follower.GetComponent<FollowerMoveControl>().SetClamps(levelMinX, levelMaxX);
		follower.GetComponent<TankController>().Init(followerTank);
		followerManager.AddFollower(follower);
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

	public void SetMovementClamps(int minX, int maxX){
		foreach(FollowerMoveControl follower in followerManager.followers){
			follower.SetClamps(minX, maxX);
		}
		leader.GetComponent<LeaderMoveControl>().SetClamps(minX, maxX);
	}
	public void SetMovementClampsToLevel(){
		SetMovementClamps(levelMinX, levelMaxX);
	}
}
