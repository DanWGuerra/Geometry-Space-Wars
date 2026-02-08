using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class EnemyHitFeedback : MonoBehaviour
{
    [Header("Flash")]
    [SerializeField] private Color hitColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Sound")]
    [SerializeField] private AudioClip hitSound;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Color originalColor;
    private Coroutine flashRoutine;

    private EnemyHealth health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<EnemyHealth>();

        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnHit += PlayFeedback;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHit -= PlayFeedback;
    }

    private void PlayFeedback()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(Flash());
        PlaySound();
    }

    private IEnumerator Flash()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void PlaySound()
    {
        if (hitSound == null) return;
        audioSource.PlayOneShot(hitSound);
    }
}
