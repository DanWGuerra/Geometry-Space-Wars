using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private GameObject Background1, Background2;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private Vector2 SpawnPosition;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Scroll(Background1);
        Scroll(Background2);
    }

    private void Scroll(GameObject Background)
    {
        Vector2 BackGroundPos = Background.transform.position;
        BackGroundPos.y -= scrollSpeed * Time.deltaTime;
        Background.transform.position = BackGroundPos;
        if(IsOutOfBounds(Background))
        {
            //reset Position
            //Debug.Log("Out of bounds");
            Background.transform.position = SpawnPosition;
        }
    }

    private bool IsOutOfBounds(GameObject Background)
    {
        Vector2 viewportPos = camera.WorldToViewportPoint(Background.transform.position);

        // Check if the object position is outside the 0-1 range
        return viewportPos.y < -0.5f;
    }
}
