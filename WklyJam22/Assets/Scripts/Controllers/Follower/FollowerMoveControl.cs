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
	float movePercentage = 0;
	bool flip = false;
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
			//MaxDistance = maxDistance;
			targetTransform = LeaderMoveControl.instance.transform;
		}

		targetPosition = targetTransform.position;
	}

	public void SetWaypoint(Transform point){
		targetTransform = point;
		MaxDistance = 0.1f;
	}
	public void CancelWaypoint(){
		targetTransform = null;
		MaxDistance = maxDistance;
		movePercentage = 0;
	}

	void Update(){
		if (hasToMove == true){
			movePercentage = speed * Time.deltaTime / distanceToTarget;
			transform.position = Vector2.Lerp(transform.position, targetPosition, movePercentage);
			//sprite_sorter.UpdateSpriteSort();
			FlipSprites(targetPosition.x);
		} 
	}
	void FlipSprites(float x){
		bool curFlip = flip;
		if (x < transform.position.x){
			curFlip = true;
		}else if (x > transform.position.x){
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
