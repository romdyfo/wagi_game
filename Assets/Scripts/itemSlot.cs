using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    private InventoryManager inventoryManager;

    private void Start(){
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    //Item slot
    [SerializeField]
    private TMP_Text quantityText;
    
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private int maxNumberOfItems;

    //Item description slot

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;

    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription){
        //check slot 
        if(isFull){
            return quantity;
        }

        //update name, quantity, image, description
        this.itemName = itemName;

        this.itemSprite = itemSprite;
        
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        
        this.itemDescription = itemDescription;

        this.quantity += quantity;

        if(this.quantity >= maxNumberOfItems){
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;

        }

        //update quantitytext

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
        
    

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right){
            OnRightClick();
        }
    }

    public void OnLeftClick(){
        if(thisItemSelected){
            
            this.quantity-=1;
            quantityText.text = this.quantity.ToString();
            if(this.quantity<=0)
                EmptySlot();
            

        }
        else {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            itemDescriptionImage.enabled = true;
        }

    }
    
    public void EmptySlot(){
        quantityText.enabled = false;
        itemDescriptionImage.enabled = false;
        itemImage.enabled = false;
        

        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";


    }

    public void OnRightClick(){

    }

    
}
