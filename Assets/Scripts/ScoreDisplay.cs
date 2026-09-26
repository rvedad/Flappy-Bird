using UnityEngine;
using System.Collections;

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
        CreateDigits();
    }

    void CreateDigits()
    {
        if (digitObjects != null) return;

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

    public void DisplayScore(int score)
    {
        CreateDigits();

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

    public void FlashGold()
    {
        StartCoroutine(FlashGoldRoutine());
    }

    IEnumerator FlashGoldRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            SetDigitColors(Color.gold);
            yield return new WaitForSeconds(0.15f);

            SetDigitColors(Color.white);
            yield return new WaitForSeconds(0.15f);
        }
    }

    void SetDigitColors(Color color)
    {
        CreateDigits();

        foreach (GameObject digit in digitObjects)
        {
            if (digit.activeSelf)
                digit.GetComponent<SpriteRenderer>().color = color;
        }
    }
}