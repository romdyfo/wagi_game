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

    private Text messageText;
    //private float messageDisplayTime = 2f;
    private float messageDisplayEndTime; // 메시지가 사라질 시간

    private Coroutine currentMessageCoroutine;

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
        messageText = GameObject.Find("MessageText")?.GetComponent<Text>();

        if (messageText == null)
        {
            Debug.LogError("MessageText is not assigned and could not be found in the scene.");
        }
    }

    void Update()
    {
        // Detect right mouse button click
        if (Input.GetMouseButtonDown(1)) // 1 is for right-click
        {
            CheckPlayerProximityAndClick();
        }

        if (messageText.gameObject.activeSelf && messageText.text != "")
        {
            //Debug.Log($"Checking time: Time.time = {Time.time}, messageDisplayEndTime = {messageDisplayEndTime}");

            if (Time.time >= t + 4)
            {
                // 메시지 숨기기
                Debug.Log("Message should hide now!");
                HideMessage();
            }
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

            string message = GetMessageForItem(itemName);
            ShowMessage(message);

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

    private string GetMessageForItem(string itemName)
    {
        return $"{itemName}을 획득했습니다.";
    }
    float t;
    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;

            //t = Time.time;

            Debug.Log("message shown");
        }
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
        }
    }
}

