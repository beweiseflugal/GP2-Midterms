using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    const string HIGHSCORE = "Highscore";
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score;

    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private int highscore;
    private void Start() {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        Enemy.OnEnemyDeath += OnAnyEnemyDeath;

        score = 0;
        scoreText.text = score.ToString();

        if (PlayerPrefs.HasKey(HIGHSCORE)) {
            highscore = PlayerPrefs.GetInt(HIGHSCORE, 0);
            highscoreText.text = $"Highscore: {highscore}";
        }
   
    }

    private void scoreAdd()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void OnAnyEnemyDeath() {
        scoreAdd();
    }


    private void GameManager_OnGameOver() {
        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(HIGHSCORE, highscore);
            PlayerPrefs.Save();
        }
        scoreText.rectTransform.localPosition = new Vector3(0, 200, 0);
        scoreText.fontSize = 250;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.color = new Color32(192, 57, 43, 255);
    }
}