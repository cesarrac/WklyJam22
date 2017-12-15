using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Selector : Selector {

	NPC_Controller nPC_Controller;

	void Awake(){
		nPC_Controller = GetComponent<NPC_Controller>();
	}
	public override void Select(){
		base.Select();
		nPC_Controller.OnSelected();
	}
}
