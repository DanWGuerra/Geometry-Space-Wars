using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class PhysicsEnemyShattered : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, 0);
        Timer += Time.deltaTime;
        if (Timer >= 0.6)
        {
            Destroy(gameObject);
        }
    }

}
