using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    [Header("Backgrounds")]
    public SpriteRenderer dayBackground;
    public SpriteRenderer nightBackground;

    [Header("Settings")]
    public int pointsPerCycle = 10;
    public float transitionDuration = 2f;

    private bool isNight = false;
    private int lastCycleScore = 0;

    void Update()
    {
        if (GameManager.instance.currentState != GameManager.GameState.Playing) return;

        int score = GameManager.instance.score;

        if (score >= lastCycleScore + pointsPerCycle)
        {
            lastCycleScore += pointsPerCycle;
            isNight = !isNight;
            StartCoroutine(TransitionBackground(isNight));
        }
    }

    IEnumerator TransitionBackground(bool toNight)
    {
        float elapsed = 0f;

        float startAlpha = toNight ? 0f : 1f;
        float endAlpha   = toNight ? 1f : 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;

            float alpha = Mathf.SmoothStep(startAlpha, endAlpha, t);
            nightBackground.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
        nightBackground.color = new Color(1, 1, 1, endAlpha);
    }
}
