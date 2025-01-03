using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public CraftingSlot[] slots; // ����ĭ ���� �迭
    public InventoryManager inventoryManager; // �κ��丮 ������
    public Sprite potionSprite; // ���� �����(����)�� ��������Ʈ
    public string potionName = "����"; // ���� ������� �̸�
    public string potionDescription = "ü���� ȸ�������ִ� ������ ����"; // ���� ����
    public int potionQuantity = 3; // ���� �� �����Ǵ� ���� ����

    public GameObject ghostPrefab; // �ͽ� ������
    public Transform spawnPoint; // �ͽ� ���� ��ġ


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

        // �ͽ� ����
        SpawnGhost();
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

    private void SpawnGhost()
    {
        if (ghostPrefab != null && spawnPoint != null)
        {
            // �ͽ� Prefab�� SpawnPoint ��ġ�� ����
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, Quaternion.identity);

            // ������ �ͽſ��� �÷��̾� ���� ����
            Ghost ghostScript = ghost.GetComponent<Ghost>();
            if (ghostScript != null)
            {
                GameObject player = GameObject.FindWithTag("Player"); // �÷��̾� ������Ʈ ã��
                if (player != null)
                {
                    ghostScript.player = player.transform;
                }
            }

            Debug.Log("�ͽ��� �����Ǿ����ϴ�!");
        }
        else
        {
            Debug.LogError("�ͽ� Prefab �Ǵ� Spawn Point�� �������� �ʾҽ��ϴ�.");
        }
    }



}
