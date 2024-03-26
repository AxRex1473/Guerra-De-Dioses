using System;
using System.Collections.Generic;


[Serializable]
public class PlayerStats 
{
    public int Health = 100;
    public string Name = "Aña Lover";
    public float BaseAttackSpeed = 1;
    public int Level = 1;
    public bool HasUnlockedSomething;
    public bool HasUnlockedSomethingElse;

 

    public List<Item> Inventory = new List<Item>() {
        new Item() {
            Value = 1,
            IsConsumable = false,
            Name = "Gold Pieces",
            Quantity = 10
        }
    };
}
