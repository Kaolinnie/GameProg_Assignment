using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    private int _currentLevel = 1;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    private int CurrentLevel {
        get {
            return _currentLevel;
        }
        set {
            if (value >= 0 && value < SceneManager.sceneCount) {
                _currentLevel = value;
            }
        }
    }

    private void LoadLevel() {
        SceneManager.LoadScene(CurrentLevel);
    }
    private void Awake() {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
    
    
    public void StartGame() {
        LoadLevel();
    }
    public void NextLevel() {
        CurrentLevel++;
        LoadLevel();
    }
    public void ExitGame() {
        Application.Quit();
    }

}
