using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public string itemName;     // 아이템 이름
    public int quantity;        // 아이템 수량
    public Sprite itemSprite;   // 아이템 이미지
    public Image slotImage;     // 슬롯의 UI 이미지

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(itemName) || quantity <= 0;
    }

    public void SetItem(string name, int qty, Sprite sprite)
    {
        Debug.Log($"SetItem called with: {name}, quantity: {qty}");

        itemName = name;
        quantity = qty;
        itemSprite = sprite;

        if (slotImage != null)
        {
            slotImage.sprite = sprite;
            slotImage.enabled = true;
        }
        else
        {
            Debug.LogError("slotImage가 연결되지 않았습니다. Inspector에서 확인하세요.");
        }
    }

    public void ClearSlot()
    {
        itemName = "";
        quantity = 0;
        itemSprite = null;

        if (slotImage != null)
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
        }

        Debug.Log($"Slot cleared: {gameObject.name}");
    }

}
