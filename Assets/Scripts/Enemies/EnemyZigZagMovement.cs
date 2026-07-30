using UnityEngine;

[RequireComponent(typeof(EnemyTarget))]
public class EnemyZigZagMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    [Header("Zig Zag")]
    [SerializeField] private float zigZagAmplitude = 1f;
    [SerializeField] private float zigZagFrequency = 3f;

    [Header("Attack")]
    [SerializeField] private float directChaseYThreshold = 2f;

    [Header("Rotation")]
    [SerializeField] private float rotationOffset = -90f; // Use 0 if your sprite faces right

    private Collider2D screenBounds;
    private EnemyTarget target;
    private float timeOffset;

    private void Awake()
    {
        target = GetComponent<EnemyTarget>();
        timeOffset = Random.value * 10f;
        screenBounds = FindAnyObjectByType<ScreenBounds>().GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (target.Target == null || screenBounds == null)
            return;

        MoveZigZag();
    }

    private void MoveZigZag()
    {
        Vector3 toPlayer = target.Target.position - transform.position;
        Vector3 direction = toPlayer.normalized;

        Vector3 movement;

        // When close to the player's Y position, dive straight at them
        if (transform.position.y <= directChaseYThreshold)
        {
            // Face the player only during the dive
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            moveSpeed = 8;
            movement = direction * moveSpeed;
        }
        else
        {
            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);

            float zigZag = Mathf.Sin((Time.time + timeOffset) * zigZagFrequency);

            movement = direction * moveSpeed +
                       perpendicular * zigZag * zigZagAmplitude;
        }

        transform.position += movement * Time.deltaTime;
        transform.position = ClampToBounds(transform.position);
    }


    private Vector3 ClampToBounds(Vector3 position)
    {
        Bounds bounds = screenBounds.bounds;

        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        return position;
    }
}