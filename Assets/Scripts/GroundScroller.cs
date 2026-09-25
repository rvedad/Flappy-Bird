using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float groundWidth;
    public Transform ground1;
    public Transform ground2;

    void Update()
    {
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
}
