using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_Follower : Selector {
	FollowerMoveControl moveControl;
	TankController tankController;
	void Awake(){
		moveControl = GetComponent<FollowerMoveControl>();
		tankController = GetComponent<TankController>();
	}
	public override void Select(){
		base.Select();
		FollowerManager.instance.SelectFollower(moveControl);
	}
	public override void DropItem(ItemDrag_Controller dragItem){
		Item item = dragItem.myItem;
		if (item == null)
			return;
		if (item.itemType != ItemType.Weapon)
			return;
		tankController.SwapWeapon(item);
		dragItem.EndDrop();
	}
}
