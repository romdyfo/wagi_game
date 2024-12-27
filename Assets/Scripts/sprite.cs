using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public float speed = 5f; // Movement speed

    void Update()
    {
        // Get input from the arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // Left/Right arrow keys
        float moveVertical = Input.GetAxis("Vertical");     // Up/Down arrow keys

        // Calculate movement direction
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Move the sprite
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
