using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Game_Manager : MonoBehaviour {
	public static Game_Manager instance {get; protected set;}
	void Awake(){
		 if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
		CheckScene();
	}

	public void StartGame(){
		SceneManager.LoadScene(1);
	}
	public void GameOver(){
	/* 	Enemy_Manager.instance.PoolAll();
		FollowerManager.instance.PoolAll(); */
		SceneManager.LoadScene(2);
	}
	public void CheckScene(){
		if (SceneManager.GetActiveScene().buildIndex == 2){
			GameObject mainMenubttn = GameObject.FindGameObjectWithTag("GameController");
			mainMenubttn.GetComponentInChildren<Button>().onClick.AddListener(() => LoadMainMenu());
		}
		else if (SceneManager.GetActiveScene().buildIndex == 0){
			GameObject buttonHolder = GameObject.FindGameObjectWithTag("GameController");
			buttonHolder.transform.GetChild(0).gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => StartGame());
			buttonHolder.transform.GetChild(1).gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => Quit());
		}
		Debug.Log("scene changed");
	}
	public void LoadMainMenu(){
		SceneManager.LoadScene(0);
	}
	public void Quit(){
		Application.Quit();
	}
}
