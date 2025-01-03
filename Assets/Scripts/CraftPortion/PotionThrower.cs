using UnityEngine;

public class PotionThrower : MonoBehaviour
{
    public GameObject potionPrefab; // 포션 Prefab
    public float throwForce = 10f; // 투척 속도
    public string potionName = "치료제"; // 포션 이름
    private InventoryManager inventoryManager;
    private Moving moving; // 캐릭터 방향 참조

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        moving = GetComponent<Moving>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowPotion();
        }
    }

    void ThrowPotion()
    {
        if (inventoryManager != null && inventoryManager.UsePotion(potionName))
        {
            GameObject potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);

            Vector2 throwDirection = GetThrowDirection();
            Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
            rb.velocity = throwDirection * throwForce;

            Debug.Log("포션을 던졌습니다!");
        }
        else
        {
            Debug.LogWarning("포션이 없습니다!");
        }
    }

    Vector2 GetThrowDirection()
    {
        float dirX = moving.directionX;
        float dirY = moving.directionY;

        if (dirX == 0 && dirY == 0)
        {
            dirX = 1;
        }

        return new Vector2(dirX, dirY).normalized;
    }

}
