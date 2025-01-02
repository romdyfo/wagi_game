using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public CraftingSlot[] slots; // 제작칸 슬롯 배열
    public InventoryManager inventoryManager; // 인벤토리 관리자
    public Sprite potionSprite; // 제작 결과물(물약)의 스프라이트
    public string potionName = "물약"; // 제작 결과물의 이름
    public string potionDescription = "체력을 회복시켜주는 강력한 물약"; // 물약 설명
    public int potionQuantity = 3; // 제작 시 생성되는 물약 수량

    public void OnCraftButtonClicked()
    {
        // 제작칸 확인
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                Debug.Log("칸에 아이템이 부족합니다!");
                return;
            }
        }

        // 제작 성공
        Debug.Log($"아이템 제작 완료! {potionQuantity}개의 물약 생성.");
        AddPotionToInventory();

        // 제작칸 비우기
        ClearCraftingSlots();
    }

    private void AddPotionToInventory()
    {
        // 물약을 인벤토리에 추가
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
        // 모든 제작칸 초기화
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
        Debug.Log("모든 제작칸이 초기화되었습니다.");
    }
}
