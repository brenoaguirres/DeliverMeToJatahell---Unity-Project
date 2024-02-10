using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check if player can open/close doors
// Gives player ways to open/close doors

public class Doors : MonoBehaviour
{
    #region Fields and Properties
    //variables to check for keys
    public Inventory playerInventory;
    string playerTag = "Player";
    public ItemData[] DoorKey;
    public int totalKeys;
    int checkCount;
    private bool check;
    public bool isLocked;

    //variables for doorOpen/Close
    Quaternion rotation;
    Quaternion closedDoor;
    Vector3 relativePosition;
    private Transform target;
    public Transform OpenPosition;
    public Transform ClosePosition;
    public float speed = 0.1f;
    private bool isRotating = false;
    private float rotationTime;
    private Rigidbody rb;
    private bool isOpen = false;
    private PlayerController playerController;
    public bool hasInteracted = false;
    public bool canCheckForInteraction = true;
    #endregion

    #region Initialization
    private void Start()
    {
        playerInventory = GameObject.FindWithTag(playerTag)
            .GetComponent<Inventory>();
        totalKeys = DoorKey.Length;
        check = false;
        playerController = GameObject.FindWithTag(playerTag)
            .GetComponent<PlayerController>();
    }
    #endregion

    private void Update() 
    {
        if (canCheckForInteraction)
        {
            canCheckForInteraction = false;
            hasInteracted = playerController.hasInteracted;
        }
    }

    #region Execution
    private void FixedUpdate()
    {
        if (hasInteracted && !isLocked)
        {
            hasInteracted = false;
            if (isOpen)
            {
                target = ClosePosition;
            }
            else
            {
                target = OpenPosition;
            }
            relativePosition = target.position - transform.position;
            rotation = Quaternion.LookRotation(new Vector3(relativePosition.x, 0, relativePosition.z));
            isRotating = true;
            if (rotationTime >= 1)
                rotationTime = 0;
            rb = GetComponent<Rigidbody>();
        }
        if (isRotating)
        {
            rotationTime += Time.deltaTime * speed;
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, rotation, rotationTime));
            if (rotationTime > 1)
            {
                isRotating = false;
                isOpen = !isOpen;
            }
        }
    }
    #endregion

    #region Methods
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == playerTag)
        {
            if (isLocked)
            {
                OpenLockedDoor();
            }
        }
    }

    public void OpenLockedDoor()
    {
        for (int i = 0; i < totalKeys; i++)
        {
            check = playerInventory.CheckInInventory(DoorKey[i].item_id);
            if (check)
                checkCount++;
            check = false;
        }
        if (checkCount == totalKeys)
        {
            isLocked = false;
        }
    }

    private IEnumerator BlockInteraction()
    {
        yield return new WaitForSeconds(2f);
        canCheckForInteraction = true;
    }
    #endregion
}
