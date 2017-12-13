using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMoveControl : MonoBehaviour {
	public float speed = 1;
	public static LeaderMoveControl instance {get; protected set;}
	Vector3 moveVector;
	GameObject curWaypoint;

	float pCurSpeed;
	float curSpeed {get{return pCurSpeed;} set{pCurSpeed = Mathf.Clamp(value, 0, speed);}}
	ObjectPool pool;
	bool flip;
	void OnEnable(){
		instance = this;
		moveVector = Vector2.zero;
		//spriteSorter = GetComponent<SpriteSorter>();
	}

	void Start(){
		Camera_Controller.instance.SetTargetAndLock(this.transform, -100, 100, -1, 4.5f);
		pool = ObjectPool.instance;
	}

	void Update(){
		if (curSpeed > 0){
			float lastX = moveVector.x;
			float newX = Input.GetAxisRaw("Horizontal");
			if (newX == 0)
				newX = lastX;
			moveVector = new Vector3(newX, Input.GetAxisRaw("Vertical"), 0);
			
			transform.position += moveVector * curSpeed * Time.deltaTime;
			if (moveVector.x != 0)
				FlipSprites(moveVector.x);
		}
		
		//spriteSorter.UpdateSpriteSort();

		if (Input.GetMouseButtonDown(1)){
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (curWaypoint != null)
				pool.PoolObject(curWaypoint);
			curWaypoint = pool.GetObjectForType("waypoint", true, mousePos);
			FollowerManager.instance.SetWayPoint(curWaypoint.transform);
		}
		if (Input.GetMouseButtonDown(0)){
			if (curWaypoint == null)
				return;

			pool.PoolObject(curWaypoint);
			FollowerManager.instance.CancelWaypoint();
		}
		if (Input.GetKeyDown(KeyCode.Q)){
			SlowDown();
		}
		if (Input.GetKeyDown(KeyCode.E)){
			SpeedUp();
		}
	}
	void FlipSprites(float x){
		bool curFlip = flip;
		if (x < 0){
			curFlip = true;
		}else if (x > 0){
			curFlip = false;
		}
		if (curFlip == flip){
			return;
		}
		flip = curFlip;
		SpriteRenderer[] sRenderers = GetComponentsInChildren<SpriteRenderer>();
		for (int i = -1; i < sRenderers.Length; i++){
			if (i == -1){
				GetComponent<SpriteRenderer>().flipX = flip;
				continue;
			}
			sRenderers[i].flipX = flip;
		}
	}
	void SpeedUp(){
		curSpeed += 0.25f;
	}
	void SlowDown(){
		curSpeed -= 0.25f;
	}
}
