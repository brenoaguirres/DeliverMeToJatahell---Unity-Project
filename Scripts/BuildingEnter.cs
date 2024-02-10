using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Used on building entrances to manage scenes

public class BuildingEnter : MonoBehaviour
{
    #region Fields and Properties
    //GUI
    GUIController _gui;
    //Timer for text to disappear
    private float timeToShowText = 2f;
    //Timer for UI to appear on screen again
    private float guiRefreshTime = 0;

    //Key Needed
    //Need Manual config
    public ItemData neededKey;

    //Player Inventory
    private Inventory playerInventory;

    //Player Tag
    private string playerTag = "Player";

    //Name of the level / Code to call level
    //Need manual config
    public string placeName = "Tooltip Name Here";
    public string SceneToCall = "Enter Scene to Call";

    //Can GameManager change scenario?
    public bool readyToChangeScenario = false;
    #endregion

    #region Execution
    private void Start()
    {
        _gui = GameObject.FindWithTag("GUIController").GetComponent<GUIController>();
    }

    private void Update()
    {
        EnterBuilding();
    }

    private void FixedUpdate()
    {
        UpdateGRT();
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == playerTag && guiRefreshTime <= 0)
        {
            playerInventory = col.GetComponent<Inventory>();
            if (neededKey != null)
            {
                bool check = playerInventory.CheckInInventory(neededKey.item_id);
                if (!check || playerInventory == null)
                {
                    StartCoroutine(_gui.ShowUITextWithGradient(timeToShowText, _gui.doNotHaveKeyText));
                    guiRefreshTime = 5f;
                }
                else
                {
                    if (guiRefreshTime <= 0)
                    {
                        readyToChangeScenario = true;
                        AskEnterBuilding();
                    }
                    guiRefreshTime = 5;
                }
            }
            else
            {
                if (guiRefreshTime <= 0)
                    {
                        readyToChangeScenario = true;
                        AskEnterBuilding();
                    }
                    guiRefreshTime = 5;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == playerTag)
        {
            if (playerInventory != null)
            {
                playerInventory = null;
            }

            _gui.HideEnterBuildingWindow();
            readyToChangeScenario = false;
        }
    }
    #endregion

    #region Methods
    //Updates UI timer
    private void UpdateGRT()
    {
        guiRefreshTime -= Time.deltaTime;
    }

    public void AskEnterBuilding()
    {
        _gui.ShowEnterBuildingWindow(placeName);
    }

    // Calls GameManager to load scene
    public void EnterBuilding()
    {
        bool playerClick = _gui.buildingWindowClick;

        if (playerClick && readyToChangeScenario)
        {
            GameManager.CallScene(SceneToCall);
        }
    }
    #endregion
}