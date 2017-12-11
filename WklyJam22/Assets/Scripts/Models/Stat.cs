using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat  {

	int value;
	public string name {get; protected set;}

	int minValue, maxValue;

	public Stat(string _name, int startValue){
		value = startValue;
		name = _name;
	}
	public Stat(string _name, int min, int max){
		value = min;
		name = _name;
		minValue = min;
		maxValue = max;
	}

	public int GetValue(int modifier = 0){
		if (maxValue > minValue){
			return Random.Range(minValue, maxValue + 1) + modifier;
		}
		return value + modifier;
	}

}
