using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Renderer))]
public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;

    private void OnEnable()
    {
        Invoke(nameof(SelfDestruct), lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageable))
            return;

        damageable.TakeDamage(damage);
        SelfDestruct();
    }

    private void OnBecameInvisible()
    {
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
        // For pooling later:
        // gameObject.SetActive(false);
    }
}
