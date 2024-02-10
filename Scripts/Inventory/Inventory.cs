using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> storedItems = new List<ItemData>();
    public List<int> storedItemsQuantity = new List<int>();
    public int InventorySpace = 12;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool AddItem(ItemData item)
    {
        int index = ReturnItemIndex((item.item_id));
        if (!CheckInInventory(item.item_id))
        {
            if (storedItems.Count >= InventorySpace)
            {
                Debug.Log("Not enough room to add item");
                return false;
            }
            storedItems.Add(item);
            storedItemsQuantity.Add(item.quantity);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        else
        {
            storedItemsQuantity[index]++;
            Debug.Log(storedItemsQuantity[index]);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        int index = ReturnItemIndex(item.item_id);
        if (!CheckInInventory(item.item_id))
        {
            throw new ArgumentException();
        }
        else
        {
            if (storedItemsQuantity[index] == 1)
            {
                storedItems.RemoveAt(index);
                storedItemsQuantity.RemoveAt(index);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
            else
            {
                storedItemsQuantity[index]--;

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
    }

    public void UseItem(ItemData item)
    {
        int index = ReturnItemIndex(item.item_id);
        if (!CheckInInventory(item.item_id))
        {
            throw new ArgumentException();
        }
        else
        {
            Item usable = Instantiate(item.prefab, transform.position + new Vector3(0, 100, 0),
                Quaternion.identity).GetComponent<Item>();
            if (usable.Use())
            {
                RemoveItem(item);
                Destroy(usable);
            }
        }
    }

    public bool CheckInInventory(int id)
    {
        for (int i = 0; i < (storedItems.Count); i++)
        {
            if (storedItems[i].item_id == id)
                return true;
        }
        return false;
    }

    public int ReturnItemIndex(int id)
    {
        for (int i = 0; i < (storedItems.Count); i++)
        {
            if (storedItems[i].item_id == id)
                return i;
        }

        return -1;
    }
}
