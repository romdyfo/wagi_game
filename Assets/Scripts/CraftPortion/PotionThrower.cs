using UnityEngine;

public class PotionThrower : MonoBehaviour
{
    public GameObject potionPrefab; // ���� Prefab
    public float throwForce = 10f; // ��ô �ӵ�
    public string potionName = "ġ����"; // ���� �̸�
    private InventoryManager inventoryManager;
    private Moving moving; // ĳ���� ���� ����

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

            Debug.Log("������ �������ϴ�!");
        }
        else
        {
            Debug.LogWarning("������ �����ϴ�!");
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
