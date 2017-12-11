using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour {

	SpriteRenderer sprite_renderer;

	void OnEnable(){
		sprite_renderer = GetComponent<SpriteRenderer>();
	}
	void Start(){
		StartCoroutine("UpdateSort");
	}
	public void UpdateSpriteSort(){
		sprite_renderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
	}
	IEnumerator UpdateSort(){
		while(true){
			UpdateSpriteSort();
			yield return new WaitForSeconds(0.25f);
			yield return null;
		}
	}
}
