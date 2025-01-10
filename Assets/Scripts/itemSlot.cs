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

    private Image paperObject; // Image 타입으로 선언
    private TMP_Text MakingProcess; // 편지 형식 텍스트 (TMP_Text)

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        var paperCanvas = GameObject.Find("PaperCanvas");
        if (paperCanvas != null)
        {
            paperObject = paperCanvas.transform.Find("Paper")?.GetComponent<Image>();
            MakingProcess = paperCanvas.transform.Find("MakingProcess")?.GetComponent<TMP_Text>(); 
        }

        if (paperObject == null)
        {
            Debug.LogWarning("Paper 오브젝트를 찾을 수 없습니다! 경로를 확인하세요.");
        }
        else
        {
            Debug.Log("Paper 오브젝트가 성공적으로 연결되었습니다.");
        }
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
                if (paperObject != null)
                {
                    // 활성화 상태를 반전
                    bool isActive = paperObject.gameObject.activeSelf;
                    paperObject.gameObject.SetActive(!isActive);
                    MakingProcess.gameObject.SetActive(!isActive);

                    if (isActive)
                    {
                        Debug.Log("Paper 오브젝트가 비활성화되었습니다.");
                    }
                    else
                    {
                        Debug.Log("Paper 오브젝트가 활성화되었습니다.");
                    }
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
}
