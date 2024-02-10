using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastItemSlot : MonoBehaviour
{
    public Image icon;
   
    public ItemData item;

    public InventoryUI inventoryUI;
    public InventorySlot inventorySlot;
    public Inventory playerInventory;
    private string playerTag = "Player";
    private string useTag = "Consumable";

    public int currentSlot = -1;

    public void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        playerInventory = GameObject.FindWithTag(playerTag).GetComponent<Inventory>();
    }

    public void Update()
    {
        if (inventoryUI.lastSelectedSlot != currentSlot)
        {
            inventorySlot = inventoryUI.slots[inventoryUI.lastSelectedSlot];
            if (inventorySlot.item.type == useTag)
            {
                currentSlot = inventoryUI.lastSelectedSlot;
            }
            else
            {
                currentSlot = -1;
            }
        }

    }

    public void AddItem(ItemData newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnSlotClick()
    {
        if (item != null && currentSlot >= 0)
        {
            inventoryUI.OnConfirmFastSlot(this.item);
        }
        else
        {
            ClearSlot();
        }
    }
}
