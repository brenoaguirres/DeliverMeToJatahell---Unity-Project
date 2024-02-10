using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory playerInventory;
    string playerTag = "Player";
    public Transform itemsParent;
    [HideInInspector]
    public InventorySlot[] slots;

    //variables that handle selection
    public int lastSelectedSlot = -1;

    //variables that opens selection_menu
    public GameObject itemDropMenu;

    //controls operation of confirmation menu
    public GameObject confirmMenu;
    private string operationType;
    private string keyItemTag = "Key Item (Story Related)";
    private string removeTag = "Remove";
    private string usableItemTag = "Consumable";
    private string useTag = "Use";

    //controls for fastSlotUsage
    public FastItemSlot fastItemSlot;
    
    void Start()
    {
        playerInventory = GameObject.FindWithTag(playerTag).GetComponent<Inventory>();
        playerInventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);
        fastItemSlot = GetComponentInChildren<FastItemSlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventory.storedItems.Count)
            {
                slots[i].AddItem(playerInventory.storedItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

            slots[i].slotIndex = i;
        }
    }

    public void SelectSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].isSelected)
            {
                slots[i].selection.gameObject.SetActive(true);
                lastSelectedSlot = i;
                itemDropMenu.gameObject.SetActive(true);
                Button[] buttons = itemDropMenu.GetComponentsInChildren<Button>(true);

                for (int j = 0; j < buttons.Length; j++)
                {
                    buttons[j].interactable = true;
                    Color colorMod = buttons[j].gameObject.GetComponent<Image>().color;
                    colorMod.a = 255;
                    buttons[j].gameObject.GetComponent<Image>().color = colorMod;
                }

                if (slots[i].item.type == usableItemTag)
                {
                    buttons[3].interactable = false;
                    Color colorMod = buttons[3].gameObject.GetComponent<Image>().color;
                    colorMod.a = 100;
                    buttons[3].gameObject.GetComponent<Image>().color = colorMod;

                    //this sets the fast item slot.
                    if (fastItemSlot.item != null)
                    {
                        fastItemSlot.ClearSlot();
                    }
                    fastItemSlot.AddItem(slots[lastSelectedSlot].item);
                }

                if (slots[i].item.type == keyItemTag)
                {
                    buttons[0].interactable = false;
                    Color colorMod = buttons[0].gameObject.GetComponent<Image>().color;
                    colorMod.a = 100;
                    buttons[0].gameObject.GetComponent<Image>().color = colorMod;
                    buttons[3].interactable = false;
                    colorMod.a = 100;
                    buttons[3].gameObject.GetComponent<Image>().color = colorMod;
                }
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected)
            {
                slots[i].selection.gameObject.SetActive(false);
                slots[i].isSelected = false;
            }
        }
    }

    public void OnUseClick()
    {
        ItemData itemToUse = playerInventory.storedItems[lastSelectedSlot];
        if (itemToUse.type == usableItemTag)
        {
            operationType = useTag;
            confirmMenu.SetActive(true);
        }
    }

    public void OnInspectClick()
    {
        Debug.Log("Inspect not implemented yet");
    }

    public void OnCombineClick()
    {
        Debug.Log("Combine not implemented yet");
    }

    public void OnDiscardClick()
    {
        ItemData itemToRemove = playerInventory.storedItems[lastSelectedSlot];
        if (itemToRemove.type != keyItemTag)
        {
            operationType = removeTag;
            confirmMenu.SetActive(true);
        }
        
    }

    public void OnConfirmYes()
    {
        if (operationType == removeTag)
        {
            ItemData itemToRemove = playerInventory.storedItems[lastSelectedSlot];
            playerInventory.RemoveItem(itemToRemove);
            confirmMenu.SetActive(false);
        }

        if (operationType == useTag)
        {
            ItemData itemToUse = playerInventory.storedItems[lastSelectedSlot];
            playerInventory.UseItem(itemToUse);
            confirmMenu.SetActive(false);
            DeselectAllSlots();
            itemDropMenu.SetActive(false);
        }
    }

    public void OnConfirmFastSlot(ItemData item)
    {
        ItemData itemToUse = item;
        if (playerInventory.storedItemsQuantity[fastItemSlot.currentSlot] <= 1)
        {
            fastItemSlot.ClearSlot();
        }
        playerInventory.UseItem(itemToUse);
        confirmMenu.SetActive(false);
        itemDropMenu.SetActive(false);
    }

    public void OnConfirmNo()
    {
        confirmMenu.SetActive(false);
        DeselectAllSlots();
        itemDropMenu.SetActive(false);
    }
}
