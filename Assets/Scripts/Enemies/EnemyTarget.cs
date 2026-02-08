using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public Transform Target { get; private set; }

    private void Awake()
    {
        GameObject player = GameObject.FindAnyObjectByType<PlayerController>().gameObject;

        if (player != null)
            Target = player.transform;
    }
}
