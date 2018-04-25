using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour {

	SpriteRenderer sprite_renderer;
	CountdownHelper sortTimer;
	void OnEnable(){
		sprite_renderer = GetComponent<SpriteRenderer>();
		sortTimer = new CountdownHelper(0.5f);
	}

	void Update(){
		sortTimer.UpdateCountdown();
		if (sortTimer.elapsedPercent >= 1){
			sprite_renderer.sortingOrder = -(Mathf.FloorToInt(transform.position.y));
		}
	}
}
