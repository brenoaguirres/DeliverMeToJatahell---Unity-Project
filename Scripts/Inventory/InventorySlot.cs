using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    [HideInInspector]
    public ItemData item;

    //variables for selection toggle
    public Image selection;
    public InventoryUI inventoryUI;
    public bool isSelected = false;

    //variables for quantityBox toggle
    public Image quantityBoxImage;
    public TMPro.TextMeshProUGUI quantityBoxText;

    //variables for updates on quantityBox
    public int slotIndex;
    Inventory playerInventory;
    string playerTag = "Player";
    string initialQuantity = "0";

    public void Awake()
    {
        quantityBoxText.text = initialQuantity;
        quantityBoxImage.gameObject.SetActive(false);
        playerInventory = GameObject.FindWithTag(playerTag).GetComponent<Inventory>();
    }

    public void AddItem (ItemData newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        quantityBoxImage.gameObject.SetActive(true);
        UpdateSlot();
    }

    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityBoxImage.gameObject.SetActive(false);
        quantityBoxText.text = "0";
    }

    public void UpdateSlot()
    {
        int textAsInt;
        int.TryParse(quantityBoxText.text, out textAsInt);
        if (playerInventory.storedItemsQuantity[slotIndex] != textAsInt)
        {
            textAsInt = playerInventory.storedItemsQuantity[slotIndex];
            quantityBoxText.text = textAsInt.ToString();
        }
    }

    public void OnSlotClick()
    {
        if (item != null)
        {
            inventoryUI.DeselectAllSlots();
            isSelected = true;
            inventoryUI.SelectSlot();
        }
    }
}
