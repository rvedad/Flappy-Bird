using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("High Score")]
    public ScoreDisplay highScoreDisplay;
    public GameObject highScoreIcon;

    private int highScore = 0;

    [Header("Score")]
    public int score = 0;
    public ScoreDisplay scoreDisplay;

    [Header("UI Panels")]
    public GameObject gameOverImage;
    public GameObject startPanel;

    [Header("Audio")]
    public AudioClip scoreSound;
    public AudioClip dieSound;
    private AudioSource audioSource;

    public enum GameState
    {
        Waiting,
        Playing,
        Dead
    }

    public GameState currentState = GameState.Waiting;

    private float deathTimer = 0f;
    private float restartDelay = 1f;
    public bool CanRestart { get; private set; } = false;


    void Awake()
    {
        gameOverImage.SetActive(false);
        instance = this;
        currentState = GameState.Waiting;
        startPanel.SetActive(true);
        scoreDisplay.gameObject.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreIcon.SetActive(false);
    }
    void Start()
    {
        highScoreDisplay.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        scoreDisplay.DisplayScore(0);
    }

    void Update()
    {
        if (currentState == GameState.Dead && !CanRestart)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= restartDelay)
                CanRestart = true;
        }
    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        startPanel.SetActive(false);
        FindAnyObjectByType<BirdController>().StartPlaying();
        FindAnyObjectByType<PipeSpawner>().StartSpawning();
        scoreDisplay.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        if (currentState != GameState.Playing) return;
        score++;
        scoreDisplay.DisplayScore(score);
        audioSource.PlayOneShot(scoreSound);

        FindAnyObjectByType<PipeSpawner>().UpdateDifficulty(score);
    }

    public void BirdDied()
    {
        if (currentState == GameState.Dead) return;
        currentState = GameState.Dead;

        FindAnyObjectByType<BirdController>().Die();
        FindAnyObjectByType<GroundScroller>().StopScrolling();
        FindAnyObjectByType<PipeSpawner>().StopSpawning();

        PipeController[] pipes = FindObjectsByType<PipeController>(FindObjectsInactive.Exclude);
        foreach (PipeController pipe in pipes)
        {
            pipe.StopScrolling();
        }

        CheckHighScore();
        gameOverImage.SetActive(true);
        audioSource.PlayOneShot(dieSound);
        if (highScoreDisplay != null)
        {
            highScoreDisplay.gameObject.SetActive(true);
            highScoreIcon.SetActive(true);
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager
            .LoadScene(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (highScoreDisplay != null)
            highScoreDisplay.DisplayScore(highScore);
    }
}