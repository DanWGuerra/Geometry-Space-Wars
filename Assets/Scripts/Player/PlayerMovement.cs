using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    public void Move(Vector2 input)
    {
        Vector3 delta = new Vector3(input.x, input.y, 0f);
        transform.position += delta * speed * Time.deltaTime;
    }
}
