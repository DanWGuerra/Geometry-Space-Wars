using UnityEngine;

public class BasicLaserWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.15f;

    private float fireCooldown;

    [SerializeField] private AudioClip shootSfx;
    public bool Fire()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
            return false;
        }
          

        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        fireCooldown = fireRate;

        return true;
    }

    private void PlayShootSound()
    {
        if (shootSfx == null) return;

        AudioSource.PlayClipAtPoint(
            shootSfx,
            transform.position,
            1f
        );
    }
}
