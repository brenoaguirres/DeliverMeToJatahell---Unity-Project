using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Journal/Collectible")]
public class CollectibleData : ScriptableObject
{
    public int col_id = Random.Range(1000, 9999);
    public string title = "New Collectible";
    public Sprite icon = null;
    public string documentText = "Please Insert Text Here...";
    public GameObject prefab = null;
}
