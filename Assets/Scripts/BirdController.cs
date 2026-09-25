using UnityEngine;

public class BirdController : MonoBehaviour
{
    [Header("Bird Settings")]
    public float flapForce = 5f;
    public float maxFallSpeed = -8f;

    [Header("Animation")]
    public Sprite wingUp;
    public Sprite wingMid;
    public Sprite wingDown;
    public float animationSpeed = 0.1f;

    [Header("Rotation")]
    public float rotateUpAngle = 30f;
    public float rotateDownAngle = -90f;
    public float rotateSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    private Sprite[] frames;
    private int currentFrame = 0;
    private float animationTimer = 0f;

    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        frames = new Sprite[] { wingUp, wingMid, wingDown, wingMid };
    }

    void Update()
    {
        if (!isAlive) return;


    }

    void Flap()
    {
        rb.linearVelocity = new Vector2(0, flapForce);
    }

    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
    }
}
