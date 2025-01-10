using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public CraftingSlot[] slots;
    public InventoryManager inventoryManager;
    public Sprite potionSprite; 
    public string potionName = "물약";
    public string potionDescription = " ";
    public int potionQuantity = 3;

    public GameObject ghostPrefab;
    public Transform spawnPoint;
    public GameObject craftingPanel; // 제작 패널 (탭)


    public void OnCraftButtonClicked()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                Debug.Log("칸에 아이템이 부족합니다!");
                return;
            }
        }

        Debug.Log($"아이템 제작 완료! {potionQuantity}개의 물약 생성.");
        AddPotionToInventory();

        ClearCraftingSlots();

        SpawnGhost();

        // 제작 탭 닫기
        if (craftingPanel != null)
        {
            craftingPanel.SetActive(false);
            Debug.Log("CraftingPanel 비활성화됨.");
        }
        else
        {
            Debug.LogError("CraftingPanel이 설정되지 않았습니다!");
        }
    }


    private void AddPotionToInventory()
    {
        if (inventoryManager != null)
        {
            int leftover = inventoryManager.AddItem(potionName, potionQuantity, potionSprite, potionDescription);
            if (leftover > 0)
            {
                Debug.Log("인벤토리가 가득 찼습니다. 물약 일부를 추가할 수 없습니다.");
            }
            else
            {
                Debug.Log($"{potionQuantity}개의 물약이 인벤토리에 추가되었습니다!");
            }
        }
        else
        {
            Debug.LogError("InventoryManager가 연결되지 않았습니다.");
        }
    }

    private void ClearCraftingSlots()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
        Debug.Log("모든 제작칸이 초기화되었습니다.");
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

            Debug.Log("귀신이 생성되었습니다!");
        }
        else
        {
            Debug.LogError("귀신 Prefab 또는 Spawn Point가 설정되지 않았습니다.");
        }
    }



}
