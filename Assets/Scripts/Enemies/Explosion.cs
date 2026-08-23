using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1.36f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<EnemyHealth>(out var enemy))
            return;

        enemy.Die();
     
    }

}
