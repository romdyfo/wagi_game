using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    private InventoryManager inventoryManager;

    // Crafting Slots
    [SerializeField]
    private CraftingSlot[] craftingSlots; // ����ĭ �迭

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Item slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private int maxNumberOfItems;

    // Item description slot
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        
        // Check slot
        if (isFull)
        {
            return quantity;
        }

        // Update name, quantity, image, description
        this.itemName = itemName;
        this.itemSprite = itemSprite;

        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.itemDescription = itemDescription;
        this.quantity += quantity;

        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            // Return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        // Update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            MoveToCraftingSlot();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void MoveToCraftingSlot()
    {
        Debug.Log($"MoveToCraftingSlot called for {itemName} with quantity {quantity}");
        foreach (var craftingSlot in craftingSlots)
        {
            if (craftingSlot.IsEmpty()) // �� ���Կ��� �߰�
            {
                craftingSlot.SetItem(itemName, 1, itemSprite); // ����ĭ�� ���� 1�� �̵�
                quantity -= 1;

                if (quantity <= 0)
                {
                    EmptySlot();
                }
                else
                {
                    quantityText.text = quantity.ToString();
                }

                break;
            }
        }
    }

    public void EmptySlot()
    {
        itemName = "";
        quantity = 0;
        itemSprite = null;
        isFull = false;

        // UI �ʱ�ȭ
        itemImage.sprite = null;
        itemImage.enabled = false;
        quantityText.text = "";
        quantityText.enabled = false;
    }

    public void OnRightClick()
    {
        // ��Ŭ�� ���� �ϴ� �ȵ�...
    }
}
