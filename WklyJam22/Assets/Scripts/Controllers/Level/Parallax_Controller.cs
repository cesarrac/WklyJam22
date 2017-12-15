using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Controller : MonoBehaviour {

	public float speed = 1;
	Vector2 lastCamPos, curCamPos;
	float parallaxScale;
	
	void Awake(){
		//parallaxScale = transform.position.z * -1;
	}
	public void Init(float scale){
		parallaxScale = scale;
		lastCamPos = Camera.main.transform.position;
	}

	void Update(){
	
		curCamPos = Camera.main.transform.position;

		float parallax = (lastCamPos.x - curCamPos.x) * parallaxScale;
		Vector2 targetPos = new Vector2(transform.position.x + parallax, transform.position.y);
		Vector2 moveVector = transform.position;
		
		moveVector = Vector2.Lerp(moveVector, targetPos, speed * Time.deltaTime);
	/* 	if (moveVector.x < 0){
			moveVector.x = 0;
		} */
		transform.position = moveVector;
		lastCamPos = curCamPos;
	}
}
