using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	public static UI_Manager instance {get; protected set;}
	public GameObject canvas;
	public GameObject gameMenu;
	ObjectPool pool;
	void Awake(){
		instance = this;
	}
	void Start(){
		pool = ObjectPool.instance;
		if (Game_Manager.instance != null){
			gameMenu.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => Game_Manager.instance.Restart());
			gameMenu.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Game_Manager.instance.Quit());
		}
	}
	public void DoDamageFX(Vector2 worldPos, int damage){
		DoDamageFX(worldPos,  damage.ToString());
	}
	public void DoDamageFX(Vector2 worldPos){
		DoDamageFX(worldPos, "Miss");
	}
	void DoDamageFX(Vector2 worldPos, string damage){
		Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
		GameObject dmgFX = pool.GetObjectForType("DamageFX", true, screenPosition);
		dmgFX.transform.SetParent(canvas.transform);
		dmgFX.GetComponentInChildren<Text>().text = damage.ToString();
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			ShowGameMenu();
		}
	}
	void ShowGameMenu(){
		if (gameMenu.activeSelf == true){
			gameMenu.SetActive(false);
			return;
		}
		gameMenu.SetActive(true);
	}
}
