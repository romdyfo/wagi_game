using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    
    private Message message;

    private Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject messageObject = GameObject.Find("MessageCanvas");
        message = messageObject.GetComponent<Message>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if ((itemSlot[i].isFull == false && itemSlot[i].itemName == itemName) || itemSlot[i].quantity == 0)
            {
                
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (message != null)
                {
                    int messageLeftOver = message.AddItem(itemName, quantity, itemSprite, itemDescription);
                    Debug.Log($"Message script processed {messageLeftOver} leftover items.");
                }
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

                }
                return leftOverItems;

            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public bool CheckItems()
    {
        bool hasItem1 = false, hasItem2 = false, hasItem3 = false;

        foreach (var slot in itemSlot)
        {
            if (slot.itemName == "수행평가 종이" && slot.quantity > 0)
            {
                hasItem1 = true;
            }
            if (slot.itemName == "연필" && slot.quantity > 0)
            {
                hasItem2 = true;
            }
            if (slot.itemName == "알약" && slot.quantity > 0)
            {
                hasItem3 = true;
            }
        }

        if (hasItem1 && hasItem2 && hasItem3)
        {
            return true;
        }
        else
            return false;
    }

    public bool UsePotion(string itemName)
    {
        Debug.Log($"Trying to use potion: {itemName}");

        foreach (var slot in itemSlot)
        {
            Debug.Log($"Slot contains: {slot.itemName} (Quantity: {slot.quantity})");

            if (string.Equals(slot.itemName.Trim(), itemName.Trim(), System.StringComparison.OrdinalIgnoreCase) && slot.quantity > 0)
            {
                slot.quantity--;
                if (slot.quantity <= 0)
                {
                    slot.EmptySlot();
                }
                Debug.Log($"Potion {itemName} used. Remaining quantity: {slot.quantity}");
                return true;
            }
            else
            {
                Debug.LogWarning($"Item name mismatch: slot={slot.itemName}, potion={itemName}");
            }
        }

        Debug.LogWarning("������ �����ϴ�!");
        return false;
    }



}
