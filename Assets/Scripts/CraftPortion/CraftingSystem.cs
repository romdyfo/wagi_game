using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public CraftingSlot[] slots;
    public InventoryManager inventoryManager;
    public Sprite potionSprite; 
    public string potionName = "����";
    public string potionDescription = " ";
    public int potionQuantity = 3;

    public GameObject ghostPrefab;
    public Transform spawnPoint;
    public GameObject craftingPanel; // ���� �г� (��)


    public void OnCraftButtonClicked()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                Debug.Log("ĭ�� �������� �����մϴ�!");
                return;
            }
        }

        Debug.Log($"������ ���� �Ϸ�! {potionQuantity}���� ���� ����.");
        AddPotionToInventory();

        ClearCraftingSlots();

        SpawnGhost();

        // ���� �� �ݱ�
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
            Debug.Log("CraftingPanel ��Ȱ��ȭ��.");
        }
        else
        {
            Debug.LogError("CraftingPanel�� �������� �ʾҽ��ϴ�!");
        }
    }


    private void AddPotionToInventory()
    {
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
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, Quaternion.identity);

            Ghost ghostScript = ghost.GetComponent<Ghost>();
            if (ghostScript != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
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
