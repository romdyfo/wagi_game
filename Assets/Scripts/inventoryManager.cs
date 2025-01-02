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

    private Text messageText;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && menuActivated){
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(Input.GetKeyDown(KeyCode.I) && !menuActivated){
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription){
        for(int i = 0; i < itemSlot.Length; i ++){
            if((itemSlot[i].isFull == false && itemSlot[i].itemName == itemName) || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName ,quantity, itemSprite, itemDescription);
                if(leftOverItems > 0 ){
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                    
                }
                return leftOverItems;
                
            }
        }
        return quantity;
    }

    public void DeselectAllSlots(){
        for(int i = 0; i < itemSlot.Length; i ++){
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public bool CheckItems()
    {
        bool hasItem1 = false, hasItem2 = false, hasItem3 = false;

        // �� �������� �κ��丮�� �ִ��� Ȯ��
        foreach (var slot in itemSlot)
        {
            if (slot.itemName == "��Ʈ" && slot.quantity > 0)
            {
                hasItem1 = true;
            }
            if (slot.itemName == "����" && slot.quantity > 0)
            {
                hasItem2 = true;
            }
            if (slot.itemName == "�˾�" && slot.quantity > 0)
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
}
