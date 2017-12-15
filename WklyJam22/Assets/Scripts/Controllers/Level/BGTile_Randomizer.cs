using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTile_Randomizer : MonoBehaviour {

	public Sprite[] bgSprites;
	void OnEnable(){
		GetComponent<SpriteRenderer>().sprite = bgSprites[Random.Range(0, bgSprites.Length)];
	}
}
