using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private Collider2D boundsCollider;

    private Collider2D playerCollider;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        if (boundsCollider == null) return;

        Bounds bounds = boundsCollider.bounds;
        Bounds playerBounds = playerCollider.bounds;

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(
            position.x,
            bounds.min.x + playerBounds.extents.x,
            bounds.max.x - playerBounds.extents.x
        );

        position.y = Mathf.Clamp(
            position.y,
            bounds.min.y + playerBounds.extents.y,
            bounds.max.y - playerBounds.extents.y
        );

        transform.position = position;
    }
}
