using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParallaxMovement : MonoBehaviour
{
    //[] the variable will hold a list of multiple values/ List of backgrounds, scroll speeds, and spawn positions for each background object
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private float[] scrollSpeed;
    [SerializeField] private Vector3[] spawnPositions; //Vector 3 z position is used to determine the order of the background objects in the scene

    private Camera cam;

    private void Start()
    {
        cam = Camera.main; // Get the main camera reference at the start 
    }

    private void FixedUpdate()
    {
        //for (i Initializes an integer variable named i starting at 0; restarts if the index is less than the number of backgrounds; adds 1 to the value of i after every loop cycle)
        for (int i = 0; i < backgrounds.Length; i++) //.Length  returns or sets the number of elements in the Array
        {
            Scroll(backgrounds[i], scrollSpeed[i], spawnPositions[i]);//Scrolls the background object at index i using the corresponding scroll speed and spawn position
        }
    }

    private void Scroll(GameObject Background, float speed, Vector3 spawnPosition)
    {
        Vector3 BackGroundPos = Background.transform.position; //Vector3 is a structure that represents a point in 3D space with x, y, and z coordinates. It is used to store the position of the background object in the scene.
        BackGroundPos.y -= speed * Time.deltaTime; //Time.deltaTime is the time in seconds it took to complete the last frame. It is used to make the movement frame rate independent, ensuring that the background scrolls at a consistent speed regardless of the frame rate.
        Background.transform.position = BackGroundPos; //Updates the position of the background object in the scene to the new position calculated above
        if (IsOutOfBounds(Background)) //Checks if the background object is out of bounds (below the camera's view)
        {
            //Debug.Log("Out of bounds");
            Background.transform.position = spawnPosition; //Resets the position of the background object to its corresponding spawn position, effectively creating a looping effect
        }
    }

    private bool IsOutOfBounds(GameObject Background) //Checks if the background object is out of bounds (below the camera's view)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(Background.transform.position); //WorldToViewportPoint converts the world position of the background object to viewport coordinates, which range from (0,0) at the bottom-left corner of the camera's view to (1,1) at the top-right corner. The z-coordinate is ignored in this case since we only care about the y-coordinate for vertical scrolling.
        return viewportPos.y < -0.5f; // Check if the object position is outside the 0-1 range
    }
}


//    private float startPos, length;
//    public GameObject cam;
//    public float parallaxEffect; // The speed at which the background should move relative to the camera
//    void Start()
//    {
//        startPos = transform.position.x;
//        length = GetComponent<SpriteRenderer>().bounds.size.x;
//    }
//    void FixedUpdate()
//    {
//        // Calculate distance background move based on cam movement
//        float distance = cam.transform.position.x * parallaxEffect; // 0 = move with cam | | 1 = won't move | | 0.5 = half
//        float movement = cam.transform.position.x * (1 - parallaxEffect);
//        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
//        // if background has reached the end of its length adjust its position for infinite scrolling
//        if (movement > startPos + length)

//            startPos += length;

//        else if (movement < startPos - length)

//            startPos -= length;
//    }
//}



//{
//    Transform cam;
//    Vector3 camStartPos;
//    float distance;

//    GameObject[] backgrounds;
//    Material[] mat;
//    float[] backSpeed;

//    float farthestBack;

//    [Range(0.01f, 0.05f)]
//    public float parallaxSpeed;

//    void Start()
//    {
//        cam = Camera.main.transform;

//        camStartPos = cam.position;

//        int backCount = transform.childCount;

//        mat = new Material[backCount];

//        backSpeed = new float[backCount];

//        backgrounds = new GameObject[backCount];

//        for (int i = 0; i < backCount; i++) //
//        {
//            backgrounds[i] = transform.GetChild(i).gameObject;
//            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
//        }

//        BackSpeedCalculate(backCount);
//    }

//    void BackSpeedCalculate(int backCount)
//    {
//        for (int i = 0; i < backCount; i++)
//        {
//            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
//            {
//                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
//            }
//        }

//        for (int i = 0; i < backCount; i++)
//        {
//            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
//        }
//    }

//    private void LateUpdate()
//    {
//        distance = cam.position.x - camStartPos.x;
//        transform.position = new Vector3(cam.position.x - 1, transform.position.y, -5);

//        for (int i = 0; i < backgrounds.Length; i++)
//        {
//            float speed = backSpeed[i] * parallaxSpeed;
//            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
//        }
//    }

//}