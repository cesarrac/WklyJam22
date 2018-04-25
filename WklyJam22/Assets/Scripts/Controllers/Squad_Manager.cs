using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad_Manager : MonoBehaviour {

	public static Squad_Manager instance {get;protected set;}
	FollowerManager followerManager;
	public GameObject leader {get; protected set;}
	public TankPrototype leaderTank, followerTank;
	
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

		new Inventory(10);
	}
	public void SpawnSquad(Vector2 leaderPos, int areaWidth){
		if (pool == null)
			pool = ObjectPool.instance;
		// Leader:
		leader = pool.GetObjectForType("Leader", true, leaderPos);
		leader.transform.SetParent(this.transform);
		leader.GetComponent<TankController>().Init(leaderTank);
		leader.GetComponent<LeaderMoveControl>().Init();
		leader.GetComponent<Leader_CombatController>().health.onHPZero += OnLeaderHPZero;
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
		follower.GetComponent<Follower_CombatController>().health.onHPZero += (hp) => OnFollowerHPZero(hp, follower);
		followerManager.AddFollower(follower);
	}
	public GameObject GetLeader(){
		return leader;
	}
	public GameObject[] GetFollowers(){
		List<GameObject>followers = new List<GameObject>();
		if (followerManager.followers.Count <= 0)
			return followers.ToArray();
		foreach(FollowerMoveControl follower in followerManager.followers){
			followers.Add(follower.gameObject);
		}
		return followers.ToArray();
	}
	void OnLeaderHPZero(int hitPoints){
		if (hitPoints > 0)
			return;
		FX_Manager.instance.DoFX(FXType.Death, leader.transform.position);
		pool.PoolObject(leader);
		// Do Game over screen
		Game_Manager.instance.GameOver();
	}
	void OnFollowerHPZero(int hitPoints, GameObject followerGobj){
		if (hitPoints > 0){
			return;
		}
		FX_Manager.instance.DoFX(FXType.Death, followerGobj.transform.position);
		followerGobj.GetComponent<Follower_CombatController>().health.onHPZero -= (hp) => OnFollowerHPZero(hp, followerGobj);
		followerGobj.transform.SetParent(null);
		pool.PoolObject(followerGobj);
		followerManager.SetFollowers();
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
