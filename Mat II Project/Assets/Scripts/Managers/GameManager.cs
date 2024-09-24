using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    [SerializeField] private GameState currentGameState = GameState.MAIN_MENU;
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }

    private bool hasGameStarted = false;
    private bool hasBackgroundMusicStarted = false;

    private GameState previousGameMenuState;

    private YesNoPromptContext nextGameMenuContext;

    private int score;
    public int Score { get { return score; } set { score = value; } }

    private int highScore;
    public int HighScore { get { return highScore; } set { highScore = value; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        currentGameState = GameState.MAIN_MENU;
        SetGameState(currentGameState);
    }


    public void SetGameState(GameState newState)
    {
        currentGameState = newState;

        switch (currentGameState)
        {
            case GameState.MAIN_MENU:
                SetTimeScaleToZero();
                hasGameStarted = false;
                SoundManager.Instance.TurnONBackgroundMusic(false);
                hasBackgroundMusicStarted = SoundManager.Instance.IsBackgroundMUsicON;
                SoundManager.Instance.TurnONSoundsSFX(true);
                LoadHighScore();
                UIController.Instance.ShowMainMenuUI();
                break;
            case GameState.PAUSE_MENU:
                SetTimeScaleToZero();
                UIController.Instance.ShowPauseMenuUI();
                break;
            case GameState.RESTART_MENU:
                SetTimeScaleToZero();
                hasGameStarted = false;
                SoundManager.Instance.TurnONBackgroundMusic(false);
                hasBackgroundMusicStarted = SoundManager.Instance.IsBackgroundMUsicON;
                SaveHighScore();
                UIController.Instance.UpdateFinalScores();
                UIController.Instance.ShowRestartMenuUI();
                break;
            case GameState.YES_NO_PROMPT:
                UIController.Instance.ShowYesNoPromptUI();
                break;
            case GameState.HUD:
                SetTimeScaleToOne();
                SoundManager.Instance.TurnONBackgroundMusic(true);
                if (!hasBackgroundMusicStarted) SoundManager.Instance.PlayBackgroundMusic(Sounds.BACKGROUND_MUSIC);
                hasBackgroundMusicStarted = SoundManager.Instance.IsBackgroundMUsicON;
                if (!hasGameStarted) StartGame();
                hasGameStarted = true;
                UIController.Instance.ShowHUDMenuUI();
                break;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void PauseGame()
    {
        SetGameState(GameState.PAUSE_MENU);
    }


    public void ResumeGame()
    {
        SetGameState(GameState.HUD);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


    private void SetTimeScaleToZero()
    {
        Time.timeScale = 0;
    }


    private void SetTimeScaleToOne()
    {
        Time.timeScale = 1;
    }


    public void OnMainMenuButtonClicked()
    {
        nextGameMenuContext = YesNoPromptContext.MAIN_MENU;
        OpenYesNoPrompt();
    }


    public void OnQuitButtonClicked()
    {
        nextGameMenuContext = YesNoPromptContext.QUIT_GAME;
        OpenYesNoPrompt();
    }


    private void OpenYesNoPrompt()
    {
        previousGameMenuState = currentGameState;

        SetGameState(GameState.YES_NO_PROMPT);
    }


    public void YesButtonPressedOnYesNoPromptMenu()
    {
        switch (nextGameMenuContext)
        {
            case YesNoPromptContext.QUIT_GAME:
                QuitGame();
                break;
            case YesNoPromptContext.MAIN_MENU:
                SetGameState(GameState.MAIN_MENU);
                SceneManager.LoadScene(0);
                break;
        }

    }


    public void NoButtonPressedOnYesNoPromptMenu()
    {
        SetGameState(previousGameMenuState);
    }


    public void UpdateScore()
    {
        UIController.Instance.UpdateScore();
    }


    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("Highscore", highScore);
    }


    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }
}
















