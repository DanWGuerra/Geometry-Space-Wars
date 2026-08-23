using System.Collections;
using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float leftPoint;
    [SerializeField] private float rightPoint;

    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombDropPoint;
    private float bombCooldown;

    private bool movingRight = true;


    private void Start()
    {
        StartCoroutine(DropBomb());
    }

    void Update()
    {
        Move();
        DropBomb();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= rightPoint)
            {
                movingRight = false;

            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= leftPoint)
            {
                movingRight = true;

            }
        }
    }

    IEnumerator DropBomb()
    {
        
        yield return new WaitForSeconds(Random.Range(2, 5));
  
            Instantiate(
                bombPrefab,
                bombDropPoint.position,
                Quaternion.identity
            );

        StartCoroutine(DropBomb());

    }
}