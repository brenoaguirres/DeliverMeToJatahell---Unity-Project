using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitMedium : HealingItem
{
    private new void Awake()
    {
        InitializeInteractable();
        HealQuantity = 50;
    }
}
