using UnityEngine;

[RequireComponent(typeof(EnemyTarget))]
public class EnemyShooter : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootInterval = 1.5f;
    [SerializeField] private float bulletSpeed = 6f;

    [Header("Optional")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private float shootVolume = 1f;

    private EnemyTarget target;
    private float shootTimer;
    private AudioSource audioSource;

    private void Awake()
    {
        target = GetComponent<EnemyTarget>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (target.Target == null)
            return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null)
            return;

        Vector3 spawnPos = firePoint != null
            ? firePoint.position
            : transform.position;

        Vector2 direction =
            (target.Target.position - spawnPos).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPos,
            Quaternion.identity
        );

        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            enemyBullet.SetDirection(direction);
        }

        if (shootSfx != null)
        {
            audioSource.PlayOneShot(shootSfx, shootVolume);
        }
    }

}
