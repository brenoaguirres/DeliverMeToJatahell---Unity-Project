using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSmall : HealingItem
{
    private new void Awake()
    {
        InitializeInteractable();
        HealQuantity = 25;
    }
}
