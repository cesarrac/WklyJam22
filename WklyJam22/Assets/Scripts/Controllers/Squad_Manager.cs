using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad_Manager : MonoBehaviour {

	public static Squad_Manager instance {get;protected set;}
	FollowerManager followerManager;
	public GameObject leader {get; protected set;}
	public TankPrototype leaderTank, followerTank;
	Inventory squad_inventory;
	public int currFollowerCount {
		get{
			if (followerManager == null)
				return 0;
			else
				return followerManager.followers.Count;
		}
	}
	ObjectPool pool;
	public ItemPrototype testPrototype, testPrototype2;
	void Awake(){
		instance = this;
		followerManager = GetComponentInChildren<FollowerManager>();

		squad_inventory = new Inventory(10);
	}
	public void SpawnSquad(Vector2 leaderPos){
		if (pool == null)
			pool = ObjectPool.instance;
		// Leader:
		leader = pool.GetObjectForType("Leader", true, leaderPos);
		leader.transform.SetParent(this.transform);
		leader.GetComponent<TankController>().Init(leaderTank);
	
		// TEST spawn follower
		GameObject follower = pool.GetObjectForType("Follower", true, leaderPos + Vector2.left);
		follower.transform.SetParent(followerManager.transform);
		follower.GetComponent<TankController>().Init(followerTank);
		followerManager.AddFollower(follower);
		/* if (currFollowerCount > 0){
			// Spawn followers
		} */

		// TEST INVENTORY:
		Item testItem = ItemDatabase.instance.CreateInstance(testPrototype);
		Item testItem2 = ItemDatabase.instance.CreateInstance(testPrototype);
		Item testItem3 = ItemDatabase.instance.CreateInstance(testPrototype2);
		squad_inventory.AddItem(testItem);
		squad_inventory.AddItem(testItem2);
		squad_inventory.AddItem(testItem3);
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
