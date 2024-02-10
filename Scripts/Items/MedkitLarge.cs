using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitLarge : HealingItem
{
    private new void Awake()
    {
        InitializeInteractable();
        HealQuantity = 75;
    }
}
