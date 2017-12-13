using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	InvUISlot[] uiSlots;
	Inventory inventory;
	
	GameObject inventoryPanel;
	void Awake(){
		inventoryPanel = transform.GetChild(0).gameObject;
	}
	void Start(){
		if (inventoryPanel.transform.childCount < 10){
			Debug.LogError("Inventory panel does not have enough slots!");
			return;
		}
		inventory = Inventory.instance;
		uiSlots = new InvUISlot[inventory.inventory_items.Length];
		for(int i = 0; i < inventory.inventory_items.Length; i++){
			Image itemImg = inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
			Text itemCountTxt = inventoryPanel.transform.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
			uiSlots[i] = new InvUISlot(itemImg, itemCountTxt);
			uiSlots[i].SetSprite(null);
			uiSlots[i].ChangeCount(0);
		}
		inventory.onInventoryChanged += OnInventoryChanged;

	}
	void OnInventoryChanged(){
		for(int i = 0; i < inventory.inventory_items.Length; i++){
			if(inventory.inventory_items[i].item == null){
				uiSlots[i].Clear();
				continue;
			}
			if (inventory.inventory_items[i].count <= 0){
				uiSlots[i].Clear();
				continue;
			}
			if (uiSlots[i].itemName == inventory.inventory_items[i].item.name){
				if (uiSlots[i].count != inventory.inventory_items[i].count){
					uiSlots[i].ChangeCount(inventory.inventory_items[i].count);
				}
			}else{
				uiSlots[i].SetItem(inventory.inventory_items[i].item.name,
								   inventory.inventory_items[i].item.sprite,
								   inventory.inventory_items[i].count);
			}
			
				
		}
	}
	public void InventoryWindowControl(){
		if (inventoryPanel.activeSelf == true)
			inventoryPanel.SetActive(false);
		else
			inventoryPanel.SetActive(true);
	}
}
public struct InvUISlot{
	
	public string itemName;
	public Sprite itemSprite;
	public Image itemImg;
	public int count;
	public Text countText;

	public InvUISlot(Image img, Text countTxt){
		countText = countTxt;
		count = 1;
		itemSprite = null;
		itemImg = img;
		itemName = string.Empty;
	}
	public void SetItem(string name, Sprite sprite, int _count){
		itemSprite = sprite;
		SetSprite(itemSprite);
		itemName = name;
		ChangeCount(_count);
	}
	public void Clear(){
		itemName = string.Empty;
		itemSprite = null;
		count = 0;
		if (itemImg != null)
			itemImg.sprite = null;
		if (countText != null)
			countText.text = string.Empty;
	}
	public void ChangeCount(int newCount){
		count = newCount;
		if (countText == null)
			return;

		countText.text = count.ToString();
		if (count <= 0)
			countText.text = string.Empty;
	}
	public void SetSprite(Sprite sprite){
		if (itemImg == null)
			return;
		itemImg.sprite = sprite;
		if (sprite == null)
			itemImg.color = Color.clear;
		else
			itemImg.color = Color.white;
	}
}
