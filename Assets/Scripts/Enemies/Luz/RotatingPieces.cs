using UnityEngine;

public class RotatingPieces : MonoBehaviour
{
    [SerializeField] private float RotationForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationForce) * Time.deltaTime);
    }
}
