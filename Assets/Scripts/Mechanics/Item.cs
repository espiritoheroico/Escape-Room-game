using UnityEngine;

[CreateAssetMenu( fileName = "Item_pref", menuName = "INVENTORY/item")]
public class Item : ScriptableObject
{
    public string itemID;
    public string itemname;
    public Sprite itemsprite;
    public string itemdescription;
}