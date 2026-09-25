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
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        frames = new Sprite[] { wingUp, wingMid, wingDown, wingMid };
    }

    void Update()
    {
        if (!isAlive) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(0, maxFallSpeed);
        }

        float targetAngle = rb.linearVelocity.y > 0 ? rotateUpAngle : rotateDownAngle;
        float angle = Mathf.LerpAngle(
            transform.eulerAngles.z,
            targetAngle,
            rotateSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(0, 0, angle);

        animationTimer += Time.deltaTime;
        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ceiling"))
        {
            GameManager.instance.BirdDied();
        }
    }
}
