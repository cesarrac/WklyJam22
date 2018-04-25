using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMoveControl : MonoBehaviour {
	float speed = 1;
	Transform targetTransform;
	Vector2 targetPosition;
	float maxDistance = 2;
	private float MaxDistance;
	bool hasToMove = false;
	float distanceToTarget = 0;
	FollowerAttack_Controller attack_Controller;
	float movePercentage = 0;
	bool flip = false;
	int clampXMin, clampXMax;
	CountdownHelper distanceCountdown;
	TimeManager timeManager;
	void OnEnable(){
		attack_Controller = GetComponent<FollowerAttack_Controller>();
	
	}
	public void Init(){
		targetTransform = LeaderMoveControl.instance.transform;
		hasToMove = false;
		movePercentage = 0;
		// Get a random max distance
		maxDistance = Random.Range(1.5f, 3f);
		MaxDistance = maxDistance;
		targetPosition = targetTransform.position;
		distanceCountdown = new CountdownHelper(1);
		timeManager = TimeManager.instance;
		//StartCoroutine("DistanceCheck");
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
		distanceCountdown.UpdateCountdown();
		if (distanceCountdown.elapsedPercent >= 1){
			DistanceCheck();
		}
		if (hasToMove == true){
			movePercentage = speed * timeManager.deltaTime / distanceToTarget;
			transform.position = Vector2.Lerp(transform.position, targetPosition, movePercentage);
			ClampMovement();
			FlipSprites(targetPosition.x);
			if (movePercentage >= 1){
				// Check if my current target contains an interactable
				if (targetTransform != null){
					if (targetTransform.gameObject.GetComponent<Interactable>() != null){
						Interactable interactable = targetTransform.gameObject.GetComponent<Interactable>();
						if (interactable.CanInteract(this.gameObject) == true){
							interactable.Interact();
							targetTransform = null;
						}
					}
				}
			}
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
	/* IEnumerator DistanceCheck(){
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
		
			yield return new WaitForSeconds(1);
			
			SetPosition();
			yield return null;
		}
	} */
	void DistanceCheck(){
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
		SetPosition();
	}
	void ClampMovement(){
		float moveX = transform.position.x;
		float moveY= transform.position.y;
		if (moveX < clampXMin){
			moveX = clampXMin;
		}
		if (moveY < 3.31f){
			moveY = 3.31f;
		}
		if (moveX > clampXMax){
			moveX = clampXMax;
		}
		if (moveY > 7){
			moveY = 7;
		}
		transform.position = new Vector2(moveX, moveY);
	}
	public void SetClamps(int minX, int maxX){
		clampXMin = minX;
		clampXMax = maxX;
	}
	/* void OnDisable(){
		StopAllCoroutines();
	} */
}
