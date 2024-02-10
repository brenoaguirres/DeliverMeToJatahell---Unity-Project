using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int item_id = 0;
    public new string name = "New Item";
    public Sprite icon = null;
    public string type = "";
    public int quantity = 0;
    public int maxQuantity = 0;
    public int buyPrice = 0;
    public string description = "A Generic Item";
    public GameObject prefab = null;

}
