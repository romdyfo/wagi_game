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
    private CraftingSlot[] craftingSlots; // 제작칸 배열

    private GameObject paper;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
         paper = GameObject.Find("Paper");

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

    private bool isMaterial(params string[] validNames)
    {
        foreach (string validName in validNames)
        {
            if (itemName == validName)
            {
                return true;
            }
        }
        return false;
    }

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
            if (isMaterial("연필", "알약", "수행평가 종이"))
            {
                MoveToCraftingSlot();
            }
            else
            {
                if (paper.activeSelf)
                {
                    paper.SetActive(false);
                }
                else
                {
                    Debug.Log("이건 노트");
                    paper.SetActive(true);
                }
            }
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
            if (craftingSlot.IsEmpty()) // 빈 슬롯에만 추가
            {
                craftingSlot.SetItem(itemName, 1, itemSprite); // 제작칸에 수량 1개 이동
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

        // UI 초기화
        itemImage.sprite = null;
        itemImage.enabled = false;
        quantityText.text = "";
        quantityText.enabled = false;
    }

    public void OnRightClick()
    {
        // 우클릭 동작 하다 안됨...
    }

    private void HandlePaperCanvas()
    {
        if (paper != null)
        {
            if (paper.activeSelf)
            {
                paper.SetActive(false);
            }
            else
            {
                Debug.Log("이건 노트");
                paper.SetActive(true);
            }
        }
    }
}
