using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalSlot : MonoBehaviour
{
    public Image icon;
    [HideInInspector]
    public CollectibleData col;

    //variables for selection toggle
    public Image selection;
    public JournalUI journalUI;
    public bool isSelected = false;

    public void AddItem (CollectibleData collectible)
    {
        col = collectible;
        icon.sprite = col.icon;
        icon.enabled = true;
    }

    public void ClearSlot ()
    {
        col = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnSlotClick()
    {
        if (col != null)
        {
            journalUI.DeselectAllSlots();
            isSelected = true;
            journalUI.SelectSlot();
            journalUI.UpdateText(col);
        }
    }

}
