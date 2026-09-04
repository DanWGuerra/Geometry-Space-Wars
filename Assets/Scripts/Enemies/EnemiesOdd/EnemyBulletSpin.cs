using UnityEngine;

public class EnemyBulletSpin : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 6f;

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= bulletLife)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }
}
