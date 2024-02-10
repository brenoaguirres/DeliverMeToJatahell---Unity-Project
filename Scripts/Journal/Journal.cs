using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{

    public List<CollectibleData> storedItems = new List<CollectibleData>();

    public int journalSpace = 12;

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;
    

    public bool AddItem(CollectibleData col)
    {
        if (storedItems.Count >= journalSpace)
        {
            Debug.Log("Not enough room to add item");
            return false;
        }
        storedItems.Add(col);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

}
