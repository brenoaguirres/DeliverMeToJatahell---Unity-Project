using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> objectList =  new List<GameObject>();

    private void OnTriggerEnter(Collider col) 
    {
        if (col.tag == "Player")
        {
            foreach (GameObject o in objectList)
            {
                o.gameObject.SetActive(!o.gameObject.activeSelf);
            }
        }
    }
}
