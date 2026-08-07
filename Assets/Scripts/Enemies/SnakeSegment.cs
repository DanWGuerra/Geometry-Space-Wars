using UnityEngine;

public class SnakeSegment : MonoBehaviour, IDamageable
{
    public SnakeEnemy owner;
    public bool isHead;

    [SerializeField] private int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            owner.OnSegmentHit(this);
        }
    }
}