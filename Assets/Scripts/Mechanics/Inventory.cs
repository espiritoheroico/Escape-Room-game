using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemlist;
    int maxslots

    public Inventory()
    {
        itemlist = new List<Item>();
    }

    public void AddItem(Item i)
    {
        itemlist.Add(i);
    }

    public void RemoveItem(Item i)
    {
        itemlist.Remove(i);
    }
}
