using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProximityToPlayerClick2D : MonoBehaviour
{

    public UnityEvent OnItemClicked; // Event triggered when the item is clicked within range
    public float proximityTolerance = 10f; // The maximum distance to count a click
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;

    private GameObject player;

    [SerializeField]
    private string playerTag = "Player"; // Tag used to identify the player GameObject



    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag(playerTag);
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        if (player == null)
        {
            Debug.LogError($"No GameObject with tag '{playerTag}' found! Please ensure the player has the correct tag.");
        }

        // Ensure the UnityEvent is initialized
        if (OnItemClicked == null)
        {
            OnItemClicked = new UnityEvent();
        }

        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        
    }

    void Update()
    {
        // Detect right mouse button click
        if (Input.GetMouseButtonDown(1)) // 1 is for right-click
        {
            CheckPlayerProximityAndClick();
        }
    }

    void CheckPlayerProximityAndClick()
    {
        // Ensure the player reference is valid
        if (player == null) return;


        // Calculate the distance between the player and the item
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);


        // Check if the distance is within the proximity tolerance
        if (distanceToPlayer <= proximityTolerance)
        {
            Debug.Log("Player is close enough! Item clicked.");


            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;

            }
            
        }
        else
        {
            Debug.Log("Player is too far from the item.");
        }
    }




    
}