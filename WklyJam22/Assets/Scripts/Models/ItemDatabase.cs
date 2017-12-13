using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase{
    public static ItemDatabase instance {get; protected set;}

    Dictionary<ItemType, List<Item>> ItemPrototypeMap;

  
    public ItemDatabase(){
        instance = this;
    }

    void InitPrototypes(){
        ItemPrototypeMap = new Dictionary<ItemType, List<Item>>();
    /*     ItemPrototypeMap.Add(ItemType.Weapon,
                            new List<Item>(){
                                new Item("Kinston Hi-Fire", ItemType.Weapon, ItemQuality.Functional)
                            }); */
    }

    Stat[] GetStatsByQuality(ItemQuality quality, Stat[] baseStat){
        int qualityMinModifier = 0;
        int qualityMaxModifier = 0;
        foreach(Stat stat in baseStat){
            // testing
            stat.Modify(qualityMinModifier, qualityMaxModifier);
        }
        return baseStat;
    }
    ItemQuality GetRandomQuality(){
        return (ItemQuality)Random.Range(0, System.Enum.GetValues(typeof(ItemQuality)).Length);
    }
    public Item CreateInstance(ItemPrototype prototype){
        ItemQuality randomQuality = GetRandomQuality();
        return Item.CreateInstance(prototype,randomQuality, GetStatsByQuality(randomQuality, prototype.baseStats));
    }

  
}

public struct ItemHolder{
    public Item myItem;
}