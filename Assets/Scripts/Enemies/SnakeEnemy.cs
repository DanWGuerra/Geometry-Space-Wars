using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform head;
    public List<Transform> segments = new();


    [Header("Movement")]
    public float verticalSpeed = 8f;     // How fast it falls
    public float waveSpeed = 5f;         // How fast the wave progresses
    public Vector3 direction = Vector3.down;

    [Header("Erratic Movement")]
    public float horizontalAmplitude = 2f;
    public float horizontalFrequency = 3f;

    public float verticalWaveAmplitude = 0.4f;
    public float verticalWaveFrequency = 6f;

    private Vector3 startPosition;
    private float travel;

    [Header("Body")]
    public float segmentSpacing = 0.35f;
    public float recordSpacing = 0.05f;

    private readonly List<Vector3> history = new();


    public SnakeSegment bodyPrefab;
    public SnakeEnemy snakePrefab;
    public int bodyCount = 6;

    private int horizontalDirection = 1;

    void Start()
    {
        startPosition = head.position;
    }

    void Update()
    {

        segments.RemoveAll(s => s == null);
        // If there are no body segments left, destroy the snake
        if (segments.Count == 0)
        {
            DestroyWholeSnake();
            return;
        }

        // Move head
        travel += waveSpeed * Time.deltaTime;

        Vector3 pos = startPosition;

        // Constant downward movement
        startPosition += Vector3.down * verticalSpeed * Time.deltaTime;
        pos = startPosition;

        // Large horizontal weaving
        pos.x += Mathf.Sin(travel * horizontalFrequency) *
         horizontalAmplitude *
         horizontalDirection;

        // Small vertical wobble
        pos.y += Mathf.Sin(travel * verticalWaveFrequency) * verticalWaveAmplitude;

        head.position = pos;

        // Record only after moving a fixed distance
        if (Vector3.Distance(history[0], head.position) >= recordSpacing)
        {
            history.Insert(0, head.position);
        }

        // Keep history from growing forever
        int maxPoints = Mathf.CeilToInt((segments.Count * segmentSpacing) / recordSpacing) + 50;

        while (history.Count > maxPoints)
            history.RemoveAt(history.Count - 1);

        // Move body
        for (int i = 0; i < segments.Count; i++)
        {
            float desiredDistance = (i + 1) * segmentSpacing;

            float point = desiredDistance / recordSpacing;

            int index = Mathf.FloorToInt(point);
            float t = point - index;

            index = Mathf.Clamp(index, 0, history.Count - 2);

            Vector3 target = Vector3.Lerp(history[index], history[index + 1], t);
            segments[i].position = target;

            // Rotate towards movement
            Vector3 dir = history[index] - history[index + 1];
            if (dir.sqrMagnitude > 0.0001f)
                segments[i].up = dir.normalized;
        }

        // Rotate head
        if (direction != Vector3.zero)
            head.up = direction.normalized;
    }

    public void OnSegmentHit(SnakeSegment segment)
    {
        if (segment.isHead)
        {
            DestroyWholeSnake();
            return;
        }


        SplitSnake(segment.transform);

    }

    void DestroyWholeSnake()
    {
        Destroy(head.gameObject);

        foreach (Transform t in segments)
            Destroy(t.gameObject);

        Destroy(gameObject);
    }


    public void SplitSnake(Transform hitSegment)
    {
        int index = segments.IndexOf(hitSegment);

        if (index < 0)
            return;

        Vector3 spawnPosition =
            hitSegment.position +
            Vector3.down * segmentSpacing * 1.5f;

        // Destroy the hit segment
        Destroy(hitSegment.gameObject);
        segments.RemoveAt(index);

        // Destroy every segment after it
        bool spawnNewSnake = false;

        while (segments.Count > index)
        {
            spawnNewSnake = true;

            Destroy(segments[index].gameObject);
            segments.RemoveAt(index);
        }

        InitializeHistory();
        if (segments.Count == 0)
        {
            DestroyWholeSnake();
            return;
        }

        // Spawn a brand new 2-segment snake
        if (spawnNewSnake)
        {
            SnakeEnemy clone = Instantiate(
                snakePrefab,
                spawnPosition,
                Quaternion.identity);

            clone.BuildSnake(2);

            clone.startPosition = spawnPosition;
            clone.travel = travel;

            // Move in the opposite horizontal direction
            clone.horizontalDirection = -horizontalDirection;

            int originalDir = horizontalDirection;

            horizontalDirection = originalDir;
            clone.horizontalDirection = -originalDir;
        }
    }



    public void InitializeHistory()
    {
        history.Clear();

        // Start with the current head position
        history.Add(head.position);

        Vector3 previous = head.position;

        // Build history from the current body positions
        foreach (Transform segment in segments)
        {
            float distance = Vector3.Distance(previous, segment.position);

            // Number of history samples between these two points
            int steps = Mathf.Max(1, Mathf.CeilToInt(distance / recordSpacing));

            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                history.Add(Vector3.Lerp(previous, segment.position, t));
            }

            previous = segment.position;
        }

        // Pad the end so we always have enough history samples
        int neededPoints =
            Mathf.CeilToInt((segments.Count * segmentSpacing) / recordSpacing) + 10;

        while (history.Count < neededPoints)
        {
            history.Add(previous);
        }
    }

    public void BuildSnake(int size)
    {
        bodyCount = size;

        segments.Clear();

        for (int i = 0; i < size; i++)
        {
            Transform body = Instantiate(bodyPrefab).transform;

            body.position = head.position;
            body.rotation = head.rotation;
            body.gameObject.SetActive(false);

            SnakeSegment segment = body.GetComponent<SnakeSegment>();
            segment.owner = this;

            segments.Add(body);
        }

        StartCoroutine(SpawnAnimation());
    }


    IEnumerator SpawnAnimation()
    {
        history.Clear();

        history.Add(head.position);

        Vector3 previous = head.position;

        foreach (Transform segment in segments)
        {
            segment.gameObject.SetActive(true);

            Vector3 target =
                previous + Vector3.up * segmentSpacing;

            float t = 0;

            while (t < 1f)
            {
                t += Time.deltaTime * 12f;

                segment.position =
                    Vector3.Lerp(previous, target, t);

                yield return null;
            }

            previous = target;
        }

        InitializeHistory();
    }


}