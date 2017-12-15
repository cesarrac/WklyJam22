using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{

    // FIX ME: This is not great, a dictionary with a list inside is pretty bad
   // public Dictionary<string, List<InventoryItem>> inventory_items {get; protected set;}
    public static Inventory instance {get; protected set;}
    public InventoryItem[] inventory_items;
   // public List<InventoryItem> inventory_items {get; protected set;}
    int maxSpaces = 10;
    int pSpacesFilled;
    int spacesFilled {get{return pSpacesFilled;}set{pSpacesFilled = Mathf.Clamp(value, 0, maxSpaces);}}
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChanged; 

    public Inventory(int maxSpace){
        instance = this;
        maxSpaces = maxSpace;
        inventory_items  = new InventoryItem[maxSpaces];
        for(int i= 0; i <inventory_items.Length; i++){
            inventory_items[i] = new InventoryItem(null);
        }
    }
    public bool AddItem(Item item, int count = 1){
        if (count == 0){
            return false;
        }
        if (item.stackCount > 1){
            int index = ContainsItem(item.name);
            if (index >= 0){
                inventory_items[index].count += count;
                if (onInventoryChanged != null)
                    onInventoryChanged();
                return true;
            }
           /*  if (item.stackCount > 1){
                if (inventory_items[item.name][0].count >= item.stackCount)
                    return false;

                inventory_items[item.name][0].count += 1;
            }
            else{
                // It is IN inventory but it does NOT stack
                inventory_items[item.name].Add(new InventoryItem(item));
            }
            if (onInventoryChanged != null)
                onInventoryChanged();
            return true; */
        }
        if (spacesFilled >= maxSpaces){
           return false;
        }
        // Find first empty space
        int emptyIndex = -1;
        for(int i= 0; i <inventory_items.Length; i++){
           if (inventory_items[i].item == null){
               emptyIndex = i;
           }
        }
        if (emptyIndex < 0){
            return false; // no empty space found
        }
        inventory_items[emptyIndex].item = item;
        inventory_items[emptyIndex].count += count;

        spacesFilled += 1;

       // inventory_items.Add(item.name, new List<InventoryItem>(){new InventoryItem(item)}); 

        if (onInventoryChanged != null)
            onInventoryChanged();
        return true;
    }
    public int ContainsItem(string itemName){
        for(int i = 0; i < inventory_items.Length; i++){
            if (inventory_items[i].item != null){
                if (inventory_items[i].item.name == itemName && inventory_items[i].count > 0){
                    return i;
                }
            }
        }
        return -1;
     /*    if (inventory_items.Count == 0)
            return false;
        if (inventory_items.ContainsKey(itemName) == false)
            return false;
        if (inventory_items[itemName].Count <= 0)
            return false;
        if (inventory_items[itemName][0].item == null)
            return false;
        if (inventory_items[itemName][0].count <= 0)
            return false;
        return true; */
    }
    public bool ContainsItem(string itemName, int count = 1){
        if (inventory_items.Length == 0)
            return false;
         for(int i = 0; i < inventory_items.Length; i++){
            if (inventory_items[i].item != null){
                if (inventory_items[i].item.name == itemName && inventory_items[i].count >= count){
                    return true;
                }
            }
         }
        return false;
    }
    public bool RemoveItem(string itemName){
        int index = ContainsItem(itemName);
        if (index < 0){
            return false;
        }
        inventory_items[index].count -= 1;
        if (inventory_items[index].count <= 0){
            // make item null
            inventory_items[index].item = null;
            
            spacesFilled -= 1;
        }

        if (onInventoryChanged != null)
            onInventoryChanged();
        return true;
     /*    if (ContainsItem(itemName) == false)
            return false;
        if (inventory_items[itemName].Count > 1){
            // If there's more than 1 item in the list,
            // this means that it is a NON-Stackable item
            // and we are holding more than 1 copy
            inventory_items[itemName].RemoveAt(0);
        }
        else{
            // this is a stacked item, so count must be reduced
            inventory_items[itemName][0].count -= 1;
            if (inventory_items[itemName][0].count <= 0){
                inventory_items[itemName].RemoveAt(0);
            }
        }
        // Check if the list under the itemName key is at count 0
        if (inventory_items[itemName].Count <= 0){
            inventory_items.Remove(itemName);
        }
        return true; */
    }
    public bool RemoveItem(int inventoryIndex){
        if (inventory_items.Length <= inventoryIndex){
            return false;
        }
        if (inventory_items[inventoryIndex].item == null)
            return false;
        if (inventory_items[inventoryIndex].count <= 0)
            return false;
        
        inventory_items[inventoryIndex].count -= 1;
        if (inventory_items[inventoryIndex].count <= 0){
            // make item null
            inventory_items[inventoryIndex].item = null;
            
            spacesFilled -= 1;
        }
        
        if (onInventoryChanged != null)
            onInventoryChanged();
        return true;
    }
    public bool RemoveItem(string itemName, int count = 1){
        if (count == 0){
            return true;
        }
        if (ContainsItem(itemName, count) == false)
            return false;

         for(int i = 0; i < inventory_items.Length; i++){
            if (inventory_items[i].item != null){
                if (inventory_items[i].item.name == itemName && inventory_items[i].count >= count){
                    inventory_items[i].count -= count;
                    if(inventory_items[i].count <= 0){
                        inventory_items[i].item = null;
                        spacesFilled -= 1;
                    }
                    
                    
                    if (onInventoryChanged != null)
                        onInventoryChanged();
                    return true;
                }
            }
         }
         return false;
    }

}

public struct InventoryItem{
    public int count;
    public Item item;
    public InventoryItem(Item _item){
        item = _item;
        count = 0;
    }
}