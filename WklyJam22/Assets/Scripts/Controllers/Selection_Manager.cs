using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Selection_Manager : MonoBehaviour {
	public static Selection_Manager instance {get; protected set;}
	public LayerMask selectionMask;

	void Awake(){
		instance = this;
	}
	public void TrySelect(){
		if (EventSystem.current.IsPointerOverGameObject() == true)
			return;
			
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, selectionMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponentInParent<Selector>() != null)
            {
                hit.collider.gameObject.GetComponentInParent<Selector>().Select();
            }
        
        }
	}
	public void TryPlaceItem(ItemDrag_Controller drag_Controller){
		if (EventSystem.current.IsPointerOverGameObject() == true)
			return;
			
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, selectionMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponentInParent<Selector>() != null)
            {
                hit.collider.gameObject.GetComponentInParent<Selector>().DropItem(drag_Controller);
            }
        
        }
	}
}
