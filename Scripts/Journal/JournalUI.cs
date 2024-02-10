using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalUI : MonoBehaviour
{

    private Journal playerJournal;

    private string playerTag = "Player";
    
    public Transform itemsParent;

    [HideInInspector]
    public JournalSlot[] slots;

    public TextMeshProUGUI docTitle;

    public TextMeshProUGUI docText;

    public void Start()
    {
        playerJournal = GameObject.FindWithTag(playerTag).GetComponent<Journal>();
        playerJournal.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<JournalSlot>(true);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerJournal.storedItems.Count)
            {
                slots[i].AddItem(playerJournal.storedItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void UpdateText(CollectibleData col)
    {
        docTitle.text = col.title;
        docText.text = col.documentText;
    }

    public void UpdateText()
    {
        docTitle.text = "";
        docText.text = "";
    }

    public void SelectSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected)
            {
                slots[i].selection.gameObject.SetActive(true);
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
    
}
