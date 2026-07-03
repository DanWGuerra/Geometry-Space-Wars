using UnityEngine;

public class EnemyShattered : MonoBehaviour
{
    
    private EnemyHealth health;

    [SerializeField] private GameObject ShateredEnemyPrefab;
    

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        
    }
   
    private void OnEnable()
    {
        if(health != null)
        health.OnDeath += PlayDestruction;
    }

    private void OnDesable()
    {
        if(health != null)
        health.OnDeath -= PlayDestruction;
    }
    private void PlayDestruction()
    {
        if (ShateredEnemyPrefab == null) { return; }

        Instantiate(
            ShateredEnemyPrefab,
            transform.position,
            Quaternion.identity
            );

        
    }
}
