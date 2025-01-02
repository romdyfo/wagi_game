using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public CraftingSlot[] slots; // ����ĭ ���� �迭
    public InventoryManager inventoryManager; // �κ��丮 ������
    public Sprite potionSprite; // ���� �����(����)�� ��������Ʈ
    public string potionName = "����"; // ���� ������� �̸�
    public string potionDescription = "ü���� ȸ�������ִ� ������ ����"; // ���� ����
    public int potionQuantity = 3; // ���� �� �����Ǵ� ���� ����

    public void OnCraftButtonClicked()
    {
        // ����ĭ Ȯ��
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                Debug.Log("ĭ�� �������� �����մϴ�!");
                return;
            }
        }

        // ���� ����
        Debug.Log($"������ ���� �Ϸ�! {potionQuantity}���� ���� ����.");
        AddPotionToInventory();

        // ����ĭ ����
        ClearCraftingSlots();
    }

    private void AddPotionToInventory()
    {
        // ������ �κ��丮�� �߰�
        if (inventoryManager != null)
        {
            int leftover = inventoryManager.AddItem(potionName, potionQuantity, potionSprite, potionDescription);
            if (leftover > 0)
            {
                Debug.Log("�κ��丮�� ���� á���ϴ�. ���� �Ϻθ� �߰��� �� �����ϴ�.");
            }
            else
            {
                Debug.Log($"{potionQuantity}���� ������ �κ��丮�� �߰��Ǿ����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("InventoryManager�� ������� �ʾҽ��ϴ�.");
        }
    }

    private void ClearCraftingSlots()
    {
        // ��� ����ĭ �ʱ�ȭ
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
        Debug.Log("��� ����ĭ�� �ʱ�ȭ�Ǿ����ϴ�.");
    }
}
