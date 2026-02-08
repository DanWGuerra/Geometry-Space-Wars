using UnityEngine;

[RequireComponent(typeof(EnemyTarget))]
public class EnemyZigZagMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 3f;

   
    [SerializeField] private float zigZagAmplitude = 1f;
    [SerializeField] private float zigZagFrequency = 3f;

   
    private Collider2D screenBounds;

    private EnemyTarget target;
    private float timeOffset;

    private void Awake()
    {
        target = GetComponent<EnemyTarget>();
        timeOffset = Random.value * 10f; // desync enemies in order to zigzag independently
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
        Vector3 toTarget = (target.Target.position - transform.position).normalized;

        Vector3 perpendicular = new Vector3(-toTarget.y, toTarget.x, 0f);
        float zigZag = Mathf.Sin((Time.time + timeOffset) * zigZagFrequency);

        Vector3 movement =
            toTarget * moveSpeed +
            perpendicular * zigZag * zigZagAmplitude;

        Vector3 newPosition = transform.position + movement * Time.deltaTime;

        newPosition = ClampToBounds(newPosition);

        transform.position = newPosition;
    }

    private Vector3 ClampToBounds(Vector3 position)
    {
        Bounds bounds = screenBounds.bounds;

        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        return position;
    }
}
