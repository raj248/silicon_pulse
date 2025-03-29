using System;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (ScoreManager.Instance == null) return;

        
        float timeSurvived = ScoreManager.Instance.TimeSurvived;
        int enemiesDefeated = ScoreManager.Instance.EnemiesDefeated;

        scoreText.text = $"Time: {timeSurvived:F2}s  |  Enemies: {enemiesDefeated}";
    }
}