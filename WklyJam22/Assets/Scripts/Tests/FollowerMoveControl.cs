using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMoveControl : MonoBehaviour {
	public float speed = 1;
	Transform targetTransform;
	Vector2 targetPosition;
	public float maxDistance = 2;
	private float MaxDistance;
	bool hasToMove = false;
	float distanceToTarget = 0;
	FollowerAttack_Controller attack_Controller;

	void Awake(){
		attack_Controller = GetComponent<FollowerAttack_Controller>();
		MaxDistance = maxDistance;
	}
	void Start(){
		targetTransform = LeaderMoveControl.instance.transform;
		SetPosition();
		StartCoroutine("DistanceCheck");
	}

	void SetPosition(){
		if (targetTransform == null){
			MaxDistance = maxDistance;
			targetTransform = LeaderMoveControl.instance.transform;
		}

		targetPosition = targetTransform.position;
	}

	public void SetWaypoint(Transform point){
		targetTransform = point;
		MaxDistance = 0;
	}
	public void CancelWaypoint(){
		targetTransform = null;
	}

	void Update(){
		if (hasToMove == true){
			transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distanceToTarget);
			//sprite_sorter.UpdateSpriteSort();
		} 
	}

	IEnumerator DistanceCheck(){
		while(true){
			distanceToTarget = (targetPosition - (Vector2)transform.position).magnitude;
			if (distanceToTarget >= MaxDistance){
				hasToMove = true;
			}else{
				if (hasToMove != false){
					hasToMove = false;
					if (Combat_Manager.instance.inCombat == true){
					attack_Controller.StartAttackCycle();
					}
				}
				
			}
			//Debug.Log("Distance: " + distanceToTarget);
			/* if (hasToMove == false){
				targetTransform = null;
			} */
			yield return new WaitForSeconds(1);
			
			SetPosition();
			yield return null;
		}
	}


}
