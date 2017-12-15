using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverControl : MonoBehaviour {

	void Start(){
		GetComponentInChildren<Button>().onClick.AddListener(() => Game_Manager.instance.LoadMainMenu());
	}
}
