using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    private int _currentLevel = 1;
    public bool hasDoubleJump;
    private readonly int maxLevels = 2;
    private int tmpScore;

    public void IncrementScore(int increment)
    {
        Score += increment;
    }

    public void DisplayUI(Text scoreText)
    {
        scoreText.text = $"Score: {Score}";
    }
    
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    private int CurrentLevel {
        get => _currentLevel;
        set {
            if (value > 0 && value <= maxLevels) {
                _currentLevel = value;
            }
        }
    }

    public int Score { get; private set; }

    public void LoadLevel()
    {
        tmpScore = Score;

        if (CurrentLevel >= maxLevels)
        {
            YouWin();
        } else SceneManager.LoadScene($"Level{CurrentLevel}");
    }

    public void RestartLevel()
    {
        Score = tmpScore;
        LoadLevel();
    }
    private void Awake() {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
    
    
    public void StartGame()
    {
        Score = 0;
        CurrentLevel = 1;
        LoadLevel();
    }
    public void NextLevel() {
        CurrentLevel++;
        LoadLevel();
    }
    public static void ExitGame() {
        Application.Quit();
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameover()
    {
        SceneManager.LoadScene("Gameover");
    }
    public void YouWin()
    {
        SceneManager.LoadScene("Win");
    }
    


}
