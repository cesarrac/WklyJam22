using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FXType {Death, Hit, Absorb}
public class FX_Manager : MonoBehaviour {

	public static FX_Manager instance {get; protected set;}
	ObjectPool pool;

	void Awake(){
		instance = this;
	}
	void Start(){
		pool = ObjectPool.instance;
	}
	public void DoFX(FXType fxType, Vector2 worldPos){
		GameObject fxObj = pool.GetObjectForType("FX", true, worldPos);
		fxObj.GetComponent<Animator>().Play(fxType.ToString());
	}
}
