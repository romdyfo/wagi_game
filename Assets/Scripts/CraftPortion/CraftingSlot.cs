using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public string itemName;     // ������ �̸�
    public int quantity;        // ������ ����
    public Sprite itemSprite;   // ������ �̹���
    public Image slotImage;     // ������ UI �̹���

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
            Debug.LogError("slotImage�� ������� �ʾҽ��ϴ�. Inspector���� Ȯ���ϼ���.");
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
