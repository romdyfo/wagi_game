using UnityEngine;

public class movement2d : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3 (x, y,0);
        transform.position += moveDirection*moveSpeed*Time.deltaTime;
    }
}
