using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int score = 0;
    public ScoreDisplay scoreDisplay;

    [Header("UI Panels")]
    public GameObject gameOverImage;

    private bool isGameOver = false;

    void Awake()
    {
        gameOverImage.SetActive(false);
        instance = this;
    }
    void Start()
    {
        scoreDisplay.DisplayScore(0);
    }

    public void AddScore()
    {
        if (isGameOver) return;
        score++;
        scoreDisplay.DisplayScore(score);
    }

    public void BirdDied()
    {
        if (isGameOver) return;
        isGameOver = true;
        FindAnyObjectByType<BirdController>().Die();
        gameOverImage.SetActive(true);
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
}