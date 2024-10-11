using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action OnGameOver;

    public event Action OnGameStart;

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        Instance = this;
    }

    public void GameStart() {
        OnGameStart?.Invoke();
        menuUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void GameOver() {
        OnGameOver?.Invoke();
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void restartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void quitGame() {
        Application.Quit();
    }
}