using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Pipe Settings")]
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    public float minGapY = -1.5f;
    public float maxGapY = 1.5f;
    public float gapSize = 3.5f;
    public float pipeHeight = 6.4f;

    private float spawnTimer = 0f;
    private bool isSpawning = false;

    [Header("Difficulty")]
    public float initialSpeed = 2f;
    public float maxSpeed = 5f;
    public float speedIncreasePerPoint = 0.1f;
    public float minSpawnInterval = 1f;
    public float spawnIntervalDecreasePerPoint = 0.05f;

    private float currentSpeed;
    private float currentSpawnInterval;

    void Start()
    {
        currentSpeed = initialSpeed;
        currentSpawnInterval = spawnInterval;
    }

    void Update()
    {
        if (!isSpawning) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        float gapY = Random.Range(minGapY, maxGapY);

        Vector3 spawnPos = new Vector3(7f, gapY, 0);

        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);

        PipeController pc = pipe.GetComponent<PipeController>();
        if (pc != null) pc.SetSpeed(currentSpeed);

        Transform pipeTop = pipe.transform.Find("PipeTop");
        Transform pipeBottom = pipe.transform.Find("PipeBottom");

        if (pipeTop != null)
            pipeTop.localPosition = new Vector3(0, gapSize / 2 + pipeHeight / 2, 0);

        if (pipeBottom != null)
            pipeBottom.localPosition = new Vector3(0, -(gapSize / 2 + pipeHeight / 2), 0);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void UpdateDifficulty(int score)
    {
        currentSpeed = Mathf.Min(
            initialSpeed + score * speedIncreasePerPoint,
            maxSpeed
        );

        currentSpawnInterval = Mathf.Max(
            spawnInterval - score * spawnIntervalDecreasePerPoint,
            minSpawnInterval
        );
    }
}