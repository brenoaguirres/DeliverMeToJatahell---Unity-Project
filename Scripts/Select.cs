using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{  
    [SerializeField]
    private GameObject _selectPanel;
    [SerializeField]
    private GameObject _inventory;
    [SerializeField]
    private GameObject _journal;
    [SerializeField]
    private GameObject _options;
    [SerializeField]
    private GameObject _equipment;

    public void SelectTurnOn()
    {
        _selectPanel.gameObject.SetActive(true);
    }

    public void SelectTurnOff()
    {
        _selectPanel.gameObject.SetActive(false);
    }

    public void InventoryTurnOn()
    {
        _selectPanel.gameObject.SetActive(false);
        _inventory.gameObject.SetActive(true);
    }

    public void InventoryTurnOff()
    {
        _selectPanel.gameObject.SetActive(false);
        _inventory.gameObject.SetActive(false);
    }

    public void JournalTurnOn()
    {
        _selectPanel.gameObject.SetActive(false);
        _journal.gameObject.SetActive(true);
    }

    public void JournalTurnOff()
    {
        _selectPanel.gameObject.SetActive(false);
        _journal.gameObject.SetActive(false);
    }

    public void OptionsTurnOn()
    {
        _selectPanel.gameObject.SetActive(false);
        _options.gameObject.SetActive(true);
    }

    public void OptionsTurnOff()
    {
        _selectPanel.gameObject.SetActive(false);
        _options.gameObject.SetActive(false);
    }

    public void EquipmentTurnOn()
    {
        _selectPanel.gameObject.SetActive(false);
        _equipment.gameObject.SetActive(true);
    }

    public void EquipmentTurnOff()
    {
        _selectPanel.gameObject.SetActive(false);
        _equipment.gameObject.SetActive(false);
    }

}
