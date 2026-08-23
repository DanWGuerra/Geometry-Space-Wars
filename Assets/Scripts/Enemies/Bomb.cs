using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private GameObject ExplosionPrefab;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(
                ExplosionPrefab,
                transform.position,
                Quaternion.identity
            );
        Destroy(gameObject);
    }
}