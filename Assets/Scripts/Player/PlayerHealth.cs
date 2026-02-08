using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private GameObject deathVfxPrefab;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private float deathSfxVolume = 1f;

    public event Action OnPlayerDied;

    private bool isAlive = true;

    public void Kill()
    {
        if (!isAlive)
            return;

        isAlive = false;

        PlayDeathEffects();
        OnPlayerDied?.Invoke();

        Destroy(gameObject);
    }

    private void PlayDeathEffects()
    {
        if (deathVfxPrefab != null)
        {
            Instantiate(deathVfxPrefab, transform.position, Quaternion.identity);
        }

        if (deathSfx != null)
        {
            AudioSource.PlayClipAtPoint(
                deathSfx,
                transform.position,
                deathSfxVolume
            );
        }
    }
}
