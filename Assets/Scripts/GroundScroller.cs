using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float groundWidth;
    public Transform ground1;
    public Transform ground2;
    private bool isScrolling = true;

    void Update()
    {
        if (!isScrolling) return;

        ground1.position += Vector3.left * scrollSpeed * Time.deltaTime;
        ground2.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (ground1.position.x <= -groundWidth)
        {
            ground1.position = new Vector3(
                ground2.position.x + groundWidth,
                ground1.position.y,
                0
            );
        }

        if (ground2.position.x <= -groundWidth)
        {
            ground2.position = new Vector3(
                ground1.position.x + groundWidth,
                ground2.position.y,
                0
            );
        }
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }
}
