using UnityEngine;

[RequireComponent(typeof(EnemyTarget))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private EnemyTarget target;

    private void Awake()
    {
        target = GetComponent<EnemyTarget>();
    }

    private void Update()
    {
        if (target.Target == null)
            return;

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (target.Target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
