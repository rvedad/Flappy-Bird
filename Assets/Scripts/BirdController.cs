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

    [Header("Audio")]
    public AudioClip flapSound;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private Sprite[] frames;
    private int currentFrame = 0;
    private float animationTimer = 0f;
    private float startY;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frames = new Sprite[] { wingUp, wingMid, wingDown, wingMid };
        startY = transform.position.y;
        rb.simulated = false;
    }

    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Waiting)
        {
            HoverBird();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                GameManager.instance.StartGame();
                Flap();
            }
            return;
        }

        if (GameManager.instance.currentState == GameManager.GameState.Dead) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        if (rb.linearVelocity.y < maxFallSpeed)
            rb.linearVelocity = new Vector2(0, maxFallSpeed);

        // Rotation
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

    void HoverBird()
    {
        float hoverY = Mathf.Sin(Time.time * 3f) * 0.3f;
        transform.position = new Vector3(
            transform.position.x,
            startY + hoverY,
            transform.position.z
        );

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
        audioSource.PlayOneShot(flapSound);
    }

    public void Die()
    {
        rb.linearVelocity = Vector2.zero;
        audioSource.PlayOneShot(hitSound);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ceiling"))
        {
            GameManager.instance.BirdDied();
        }
    }

    public void StartPlaying()
    {
        rb.simulated = true;
    }
}
