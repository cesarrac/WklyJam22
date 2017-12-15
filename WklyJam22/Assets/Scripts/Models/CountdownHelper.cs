using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownHelper  {

	TimeManager timeManager;
	float timeToCount, timer;
	public float elapsedPercent;
	public CountdownHelper(float _timeToCount){
		timeToCount = _timeToCount;
		timeManager = TimeManager.instance;
	}
	public void UpdateCountdown(){
		if (timer >= timeToCount){
			timer = 0;
			elapsedPercent = 0;
		}else{
			timer += timeManager.deltaTime;
			elapsedPercent = timer / timeToCount;
		}
	}
}
