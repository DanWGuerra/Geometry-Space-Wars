using Apamate.BulletHellSystem;
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

    private void OnTriggerEnter2D(Collider2D other) //OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only). This function is called on all scripts attached to the object that has a Collider2D component
    {
        //For the Weird bullet, Odette added this check to see if the other object has a WeirdBullet component, and if it does, it checks if the WeirdBullet can be destroyed. If it can, it disables the WeirdBullet and destroys the bullet If the other object does not have a WeirdBullet component, it checks if it has an IDamageable component, and if it does, it takes damage and destroys the bullet

        if (other.TryGetComponent<WeirdBullet>(out var weirdBullet)) // Check if the other object has a WeirdBullet component
        {
            if (weirdBullet.CanBeDestroyed)// Check if the WeirdBullet can be destroyed
            {
                weirdBullet.Disable();// Disable the WeirdBullet
                SelfDestruct();// Destroy the bullet
            }

            return; //return to avoid further processing if the other object is a WeirdBullet, regardless of whether it was destroyed or not
        }

        // Check if the other object has an IDamageable component in the hierarchy, and if it does, take damage and destroy the bullet

        if (!other.TryGetComponent<IDamageable>(out var damageable)) //reference to the IDamageable interface, which is implemented by the EnemyHealth class. This allows the bullet to interact with any object that can take damage, not just enemies
        {
            return; //If the other object does not have an IDamageable component, exit the method}
            //damageable.TakeDamage(damage);
            //SelfDestruct();
        }

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
