using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : Interactable
{
    public List<ItemData> items;

    private List<GameObject> storeEntries;

    public GameObject storeEntryPrefab;
    public GameObject storeUI;
    public GameObject storeParent;
    public GUIController gui;
    public Inventory inv;
    public AudioClip buySFX;
    public AudioClip notSFX;

    private void Start()
    {
        // Initialization
        gui = GameObject.FindObjectOfType<GUIController>();
        inv = GameObject.FindObjectOfType<Inventory>();

        int countIndex = 0;
        foreach (ItemData item in items)
        {
            var entry = Instantiate(storeEntryPrefab, transform.position, Quaternion.identity);
            entry.transform.SetParent(storeParent.transform);
            StoreEntry strEnt = entry.GetComponent<StoreEntry>();
            strEnt.SetEntry(countIndex, item.icon, item.name, item.buyPrice, item);
            countIndex++;
        }
    }

    public override void Interaction()
    {
        storeUI.SetActive(true);
    }

    public void CloseStore()
    {
        storeUI.SetActive(false);
    }

    
}
