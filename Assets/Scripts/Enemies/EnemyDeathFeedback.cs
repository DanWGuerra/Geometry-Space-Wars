using UnityEngine;

public class EnemyDeathFeedback : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;

    private EnemyHealth health;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnDeath += PlayDeathFeedback;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= PlayDeathFeedback;
    }

    private void PlayDeathFeedback()
    {
        SpawnExplosion();
        PlaySound();
    }

    private void SpawnExplosion()
    {
        if (explosionPrefab == null) return;

        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );
    }

    private void PlaySound()
    {
        if (deathSound == null) return;

        AudioSource.PlayClipAtPoint(
            deathSound,
            transform.position
        );
    }
}
