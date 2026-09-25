using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Digit Sprites")]
    public Sprite[] digitSprites;

    [Header("Settings")]
    public float digitSpacing = 0.6f;
    public float digitScale = 3f;

    private GameObject[] digitObjects;
    private int maxDigits = 4;

    void Awake()
    {
        digitObjects = new GameObject[maxDigits];

        for (int i = 0; i < maxDigits; i++)
        {
            GameObject digit = new GameObject("Digit" + i);
            digit.transform.SetParent(transform);
            digit.transform.localScale = Vector3.one * digitScale;
            SpriteRenderer sr = digit.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "UI";
            sr.sortingOrder = 10;
            digitObjects[i] = digit;
            digit.SetActive(false);
        }
    }

    void Start()
    {
        DisplayScore(0);
    }
    public void DisplayScore(int score)
    {
        string scoreStr = score.ToString();

        foreach (GameObject d in digitObjects)
            d.SetActive(false);

        float totalWidth = (scoreStr.Length - 1) * digitSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < scoreStr.Length; i++)
        {
            int digitValue = int.Parse(scoreStr[i].ToString());

            GameObject digitObj = digitObjects[i];
            digitObj.SetActive(true);
            digitObj.GetComponent<SpriteRenderer>().sprite = digitSprites[digitValue];
            digitObj.transform.localPosition = new Vector3(startX + i * digitSpacing, 0, 0);
        }
    }
}