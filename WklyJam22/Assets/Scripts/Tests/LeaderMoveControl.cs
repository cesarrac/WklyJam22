using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMoveControl : MonoBehaviour {
	public float speed = 1;
	public static LeaderMoveControl instance {get; protected set;}
	Vector3 moveVector;
	public GameObject waypoint;
	GameObject curWaypoint;

	float pCurSpeed;
	float curSpeed {get{return pCurSpeed;} set{pCurSpeed = Mathf.Clamp(value, 0, speed);}}
//	SpriteSorter spriteSorter;
	void OnEnable(){
		instance = this;
		//spriteSorter = GetComponent<SpriteSorter>();
	}

	void Start(){
		Camera_Controller.instance.SetTargetAndLock(this.transform, -100, 100, -1, 4.5f);
	}

	void Update(){
		if (curSpeed > 0){
			moveVector = new Vector3(1, Input.GetAxisRaw("Vertical"), 0);
			transform.position += moveVector * curSpeed * Time.deltaTime;
		}
		
		//spriteSorter.UpdateSpriteSort();

		if (Input.GetMouseButtonDown(1)){
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			curWaypoint = Instantiate(waypoint, mousePos, Quaternion.identity) as GameObject;
			FollowerManager.instance.SetWayPoint(curWaypoint.transform);
		}
		if (Input.GetMouseButtonDown(0)){
			if (curWaypoint == null)
				return;

			Destroy(curWaypoint);
			FollowerManager.instance.CancelWaypoint();
		}
		if (Input.GetKeyDown(KeyCode.A)){
			SlowDown();
		}
		if (Input.GetKeyDown(KeyCode.D)){
			SpeedUp();
		}
	}

	void SpeedUp(){
		curSpeed += 0.25f;
	}
	void SlowDown(){
		curSpeed -= 0.25f;
	}
}
