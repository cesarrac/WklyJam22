using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoveController : MonoBehaviour {

	float timer,waitToMoveTime, moveDuration;
	bool canMove = false;
	float speed = 1;
	Vector2 moveDirection;
	int minX, maxX;
	bool flip;
	TimeManager timeManager;
	public void Init(float waitTime, float duration, float moveSpeed){
		moveDuration = duration;
		waitToMoveTime = waitTime;
		speed = moveSpeed;
		moveDirection = Vector2.left;
		timeManager = TimeManager.instance;
	}
	public void SetMovementClamps(int left, int right){
		minX = left;
		maxX = right;
	}
	void Update(){
		if (waitToMoveTime <= 0)
			return;
		if (canMove == false){
			if (timer >= waitToMoveTime){
				timer = 0;
				canMove = true;
			}
			else{
				timer += timeManager.deltaTime;
			}
		}else{
			Move();
		}
	
	}
	void Move(){
		Vector2 movePosition = transform.position;
		movePosition += moveDirection * speed * timeManager.deltaTime;
		if (movePosition.x < minX){
			movePosition.x = minX;
			ChangeDirection();
		}
		if (movePosition.y < 4.5f){
			movePosition.y = 4.5f;
		}
		if (movePosition.x > maxX){
			movePosition.x = maxX;
			ChangeDirection();
		}
		FlipSprites(movePosition.x);
		transform.position = movePosition;
		UpdateMoveDuration();
	}
	void UpdateMoveDuration(){
		if (timer >= moveDuration){
			timer = 0;
			canMove = false;
			ChangeDirection();
		}else{
			timer += timeManager.deltaTime;
		}
	}
	void ChangeDirection(){
		if (moveDirection == Vector2.left)
			moveDirection = Vector2.right;
		else
			moveDirection = Vector2.left;
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
}
