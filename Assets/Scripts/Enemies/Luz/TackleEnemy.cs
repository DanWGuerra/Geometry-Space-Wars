using Cinemachine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TackleEnemy : MonoBehaviour
{
    [SerializeField] private float ImpulseForce;
    [SerializeField] private float Timer;
    [SerializeField] private Transform playerPos;
    [SerializeField] private AudioSource AhShoot;
    private Vector3 follow;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
       
    }

  
    void Update()
    {
        PlayerController playerScript = FindAnyObjectByType<PlayerController>();
        if (playerScript != null)
        {
            playerPos = playerScript.transform;
        }

        Timer += Time.deltaTime;
        if(Timer < 2)
        {
            follow = (playerPos.position - transform.position).normalized;
        }
        if(Timer >= 2 && playerScript != null)
        {
            transform.Translate(follow * ImpulseForce * Time.deltaTime, Space.World);
        }
        //transform.Translate(Vector3.down * ImpulseForce * Time.deltaTime);
    }
}
