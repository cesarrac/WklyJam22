using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuButton_Helper : MonoBehaviour {

	Game_Manager game_Manager;
	public Button restartButton, quitButton, startButton;
	void Start(){
		game_Manager = Game_Manager.instance;
		if (game_Manager != null)
			CheckScene();
		//GetComponentInChildren<Button>().onClick.AddListener(() => Game_Manager.instance.LoadMainMenu());
	}
	public void CheckScene(){
		if (SceneManager.GetActiveScene().buildIndex == 2){
			if (restartButton != null)
				restartButton.onClick.AddListener(() => game_Manager.Restart());
		}
	 	else if (SceneManager.GetActiveScene().buildIndex == 0){
			if (startButton != null){
				startButton.onClick.RemoveAllListeners();
				startButton.onClick.AddListener(() =>  game_Manager.StartGame());
			}
			if (quitButton != null){
				quitButton.onClick.RemoveAllListeners();
				quitButton.onClick.AddListener(() =>  game_Manager.Quit());
			}
			
		} 
		Debug.Log("scene changed");
	}
}
