using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterSpin : MonoBehaviour
{
    enum SpawnerType { Straight, Spinning }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate;

    private GameObject spawnedBullet;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spinning) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z+1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if(bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<EnemyBulletSpin>().speed = speed;
            spawnedBullet.GetComponent<EnemyBulletSpin>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
