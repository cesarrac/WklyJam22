using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	public static UI_Manager instance {get; protected set;}
	public GameObject canvas;
	ObjectPool pool;
	void Awake(){
		instance = this;
	}
	void Start(){
		pool = ObjectPool.instance;
	}
	public void DoDamageFX(Vector2 worldPos, int damage){
		Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
		GameObject dmgFX = pool.GetObjectForType("DamageFX", true, screenPosition);
		dmgFX.transform.SetParent(canvas.transform);
		dmgFX.GetComponentInChildren<Text>().text = damage.ToString();
	}
}
