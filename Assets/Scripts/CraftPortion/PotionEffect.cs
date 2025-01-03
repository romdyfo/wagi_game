using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            Ghost ghost = collision.GetComponent<Ghost>();
            if (ghost != null)
            {
                ghost.TakeDamage();
            }

            Destroy(gameObject);
        }
    }
}
