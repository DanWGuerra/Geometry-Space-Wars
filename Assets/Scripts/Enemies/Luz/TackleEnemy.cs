using System.Collections;
using UnityEngine;

public class TackleEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TackleRoutine());
    }

    IEnumerator TackleRoutine()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Tacleando");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
