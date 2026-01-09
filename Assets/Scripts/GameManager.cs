using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public enum GameState
{
    [InspectorName( "Gameplay" )] GAME,
    [InspectorName( "Pause" )] PAUSE_MENU,
    [InspectorName( "Level completed (either successfully or failed)" )] LEVEL_COMPLETED
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState=GameState.GAME;
    public static GameManager instance;
    private int score=0;

    public GameObject NapisPause;
    public GameObject NapisWin;

    private int keysFound=0;
    public int maxKeysNumber=3;
    public bool keysCompleted = false;

    public int lives = 3;
    public Image[] livesTab;

    public Canvas inGameCanvas;
    public TMP_Text scoreText;
    public Image[] keysTab;

    public Canvas pauseMenuCanvas;

    private float timer=0;
    public TMP_Text timerText;

    private int enemiesKilled=0;
    public TMP_Text enemiesKilledText;

    public Canvas levelCompletedCanvas;
    private const string keyHighScore="HighScoreLevel1";
    public TMP_Text highScoreText;
    public TMP_Text endScreenScoreText;
    public bool hasGoldPlatform = false;
    public void AddKeys(int keyNumber)
    {
        if (keyNumber==0)
        {
                keysTab[keyNumber].color=Color.red;
        }
        else  if (keyNumber==1)
        {
            keysTab[keyNumber].color=Color.green;
        }
        else  if (keyNumber==2)
        {
            keysTab[keyNumber].color=Color.blue;
        }
    
        keysFound++;
        if (keysFound==maxKeysNumber)
        {
            keysCompleted=true;
            Debug.Log("You have collected all keys");
        }
    }

    public void AddLives(int livesNumber)
    {
        if (lives<=3 )
        {
            if (livesNumber<0)
            {
                livesTab[lives-1].enabled=false;
            }
            else if (livesNumber>0)
            {
                livesTab[lives].enabled=true;
            }
            lives+=livesNumber;
        }
        
    }
    
    void SetGameState(GameState newGameState)
    {
        if (newGameState==GameState.LEVEL_COMPLETED)
        {
            Scene currentScene=SceneManager.GetActiveScene();
            if (currentScene.name=="Level 1")
            {
                int highScore=PlayerPrefs.GetInt(keyHighScore);
                if (highScore<score)
                {
                    highScore=score;
                    PlayerPrefs.SetInt(keyHighScore,highScore);
                }
                endScreenScoreText.text=score.ToString();
                highScoreText.text=highScore.ToString();
            }
        }
        currentGameState=newGameState;
        pauseMenuCanvas.enabled=(currentGameState==GameState.PAUSE_MENU);
        levelCompletedCanvas.enabled=(currentGameState==GameState.LEVEL_COMPLETED);
        if (newGameState==GameState.GAME)
        {
            inGameCanvas.enabled=true;
        }
        else
        {
            inGameCanvas.enabled=false;
        }
    }


    public void InGame()
    {
        SetGameState(GameState.GAME);
    }

     public void PauseMenu()
    {
        SetGameState(GameState.PAUSE_MENU);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.LEVEL_COMPLETED);
    }

    public void GameOver()
    {
        SetGameState(GameState.LEVEL_COMPLETED);
    }

    void Awake()
    {
        if(!(PlayerPrefs.HasKey(keyHighScore)))
        {
            PlayerPrefs.SetInt(keyHighScore,0);
        }
        for(int i=0;i<=2;i++)
        {
            keysTab[i].color=Color.grey;
        }
        
        enemiesKilledText.text=enemiesKilled.ToString();
        scoreText.text=score.ToString();
        if (instance==null)
        {
            instance=this;
        }
        else
        {
            Debug.LogError( "Duplicated Game Manager", gameObject );
        }
        InGame();
    }

    public void AddPoints(int points)
    {
        score+=points;
        scoreText.text=score.ToString();
    }

    public void AddEnemiesKilled()
    {
        enemiesKilled+=1;
        enemiesKilledText.text=enemiesKilled.ToString();
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }
    
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {

    }

    void Update()
    {
        timer+=Time.deltaTime;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerText.text= string.Format("{0:00}:{1:00}", minutes, seconds);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState==GameState.GAME)
            {
                PauseMenu();
                Debug.Log("Pause");
            }
            else
            {
                InGame();
                Debug.Log("Playtime");
            }
        }
    }
}
