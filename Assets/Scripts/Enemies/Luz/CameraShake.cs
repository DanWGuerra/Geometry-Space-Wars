using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource ImpulseSource;
    private CinemachineImpulseListener Listener;
    private EnemyHealth health;
    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }
    private void OnEnable()
    {
        if (health != null)
            health.OnDeath += Shaking;
    }
    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= Shaking;
    }
    private void Shaking()
    {
        ImpulseSource.GenerateImpulse();
    }
    
}
