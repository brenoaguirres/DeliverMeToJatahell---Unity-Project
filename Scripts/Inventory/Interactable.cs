using System;
using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// This class has to implement the InitializeInteractionText() at Start(), and Initialize() functions to work properly;
/// </summary>

public class Interactable : MonoBehaviour
{
    public float radius = 1.5f;
    public string playertag = "Player";
    public PlayerController playerController;
    public Journal playerJournal;
    public Inventory playerInventory;

    public TextMeshProUGUI intTxt;
    public GameObject intTxtPrefab;
    private string path = "Prefabs/InteractText";

    public SphereCollider playerCollided;

    public void Awake()
    {
        InitializeInteractable();
    }

    public void Update()
    {
        CheckForInteraction();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == playertag)
        {
            intTxt.gameObject.SetActive(true);
            playerController = col.GetComponent<PlayerController>();
            playerInventory = col.GetComponent<Inventory>();
            playerController.canInteract = true;
            playerCollided = col as SphereCollider;
            playerJournal = col.GetComponent<Journal>();
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == playertag)
        {
            intTxt.gameObject.SetActive(false);
            if (playerController != null)
            {
                playerController.canInteract = false;
                playerController = null;
            }
            if (playerCollided != null)
            {
                playerCollided = null;
            }
            if (playerInventory != null)
            {
                playerInventory = null;
            }
        }
    }
    public virtual void Interaction()
    {
        throw new NotImplementedException();
    }

    public void InitializeInteractionText()
    {
        intTxtPrefab = Resources.Load<GameObject>(path);

        GameObject referenceToChild = Instantiate(intTxtPrefab, new Vector3
            (transform.position.x, transform.position.y + 2.5f, transform.position.z), Quaternion.identity);
        referenceToChild.transform.SetParent(gameObject.transform, true);

        intTxt = GetComponentInChildren<TextMeshProUGUI>();
        intTxt.gameObject.SetActive(false);
        if (!gameObject.TryGetComponent(typeof(SphereCollider), out Component component))
        {
            gameObject.AddComponent(typeof(SphereCollider));
            gameObject.GetComponent<SphereCollider>().radius = radius;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().radius = radius;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    public void InitializePlayerController()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag(playertag)
                .GetComponent<PlayerController>();
        }
    }

    //This corrects null exception when using item from inventory
    public void InitializeInteractable()
    {
        InitializePlayerController();
        InitializeInteractionText();
    }

    public void CheckForInteraction()
    {
        if (playerController != null && playerInventory != null)
        {
            if (playerController.hasInteracted)
            {
                Interaction();
                playerController.hasInteracted = false;
            }
        }
    }
}