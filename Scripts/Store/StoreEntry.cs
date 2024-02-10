using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreEntry : MonoBehaviour
{
    public int myIndex;
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI value;
    [HideInInspector]
    public int valueInt;
    [HideInInspector]
    public ItemData item;
    [HideInInspector]
    public Store store;

    public void Start ()
    {
        store = GameObject.FindObjectOfType<Store>();
    }

    public void SetEntry(int myIndex, Sprite icon, string itemName, int value, ItemData item)
    {
        this.myIndex = myIndex;
        this.icon.sprite = icon;
        this.itemName.text = itemName;
        this.value.text = value.ToString();
        this.valueInt = value;
        this.item = item;
    }

    public void OnBuyClick()
    {
        Debug.Log(valueInt);
        Debug.Log(store.gui.KilledZombies);
        if (store.gui.KilledZombies >= valueInt)
        {
            store.gui.UpdateKilledZombies(valueInt);
            store.inv.AddItem(item);
            AudioController.instance.PlayOneShot(store.buySFX);
            store.items.RemoveAt(myIndex);
            Destroy(gameObject);
        }
        else
        {
            AudioController.instance.PlayOneShot(store.notSFX);
        }
    }
}
