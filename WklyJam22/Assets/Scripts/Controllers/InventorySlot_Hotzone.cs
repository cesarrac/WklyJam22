using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot_Hotzone : MonoBehaviour, IPointerClickHandler {
	InventoryUI inventoryUI;
	int childIndex;
	void OnEnable(){
		inventoryUI = transform.parent.gameObject.GetComponentInParent<InventoryUI>();
		childIndex = transform.GetSiblingIndex();
	}
	
    void IPointerClickHandler.OnPointerClick(PointerEventData pointer)
    {
		Debug.Log("CLICKED");
        inventoryUI.SetHotZone(childIndex);
    }
}
