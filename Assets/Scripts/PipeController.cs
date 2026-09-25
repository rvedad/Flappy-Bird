using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float scrollSpeed = 3f;
    private bool scored = false;
    private bool isScrolling = true;
    void Update()
    {
        if (!isScrolling) return;

        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;


        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bird") && !scored)
        {
            scored = true;
            GameManager.instance.AddScore();
        }
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }
}