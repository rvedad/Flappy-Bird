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

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
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

        Transform pipeTop    = pipe.transform.Find("PipeTop");
        Transform pipeBottom = pipe.transform.Find("PipeBottom");

        if (pipeTop != null)
            pipeTop.localPosition = new Vector3(0, gapSize / 2 + pipeHeight / 2, 0);

        if (pipeBottom != null)
            pipeBottom.localPosition = new Vector3(0, -(gapSize / 2 + pipeHeight / 2), 0);
    }
}