using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance { get => instance; set => instance = value; }


    [SerializeField] private UIModel uiModel;
    [SerializeField] private UIView uiView;


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


    private void OnEnable()
    {
        KeyGameEvents.OnDIMValueUpdated += UpdateDIMValue;
    }


    private void OnDisable()
    {
        KeyGameEvents.OnDIMValueUpdated -= UpdateDIMValue;
    }


    public void ShowMainMenuUI()
    {
        uiView.ShowMainMenuUI();
    }


    public void ShowPauseMenuUI()
    {
        uiView.ShowPauseMenuUI();
    }


    public void ShowRestartMenuUI()
    {
        uiView.ShowRestartMenuUI();
    }


    public void ShowHUDMenuUI()
    {
        uiView.ShowHUDMenuUI();
    }


    public void ShowYesNoPromptUI()
    {
        uiView.ShowYesNoPromptUI();
    }


    public void HideAllUI()
    {
        uiView.HideAllUI();
    }


    public void StartOrRestartGame()
    {
        GameManager.Instance.SetGameState(GameState.HUD);
    }


    public void ResumeGame()
    {
        GameManager.Instance.SetGameState(GameState.HUD);
    }


    public void OnMainMenuButtonClicked()
    {
        GameManager.Instance.OnMainMenuButtonClicked();
    }


    public void OnQuitButtonClicked()
    {
        GameManager.Instance.OnQuitButtonClicked();
    }


    public void YesButtonPressedOnYesNoPromptMenu()
    {
        GameManager.Instance.YesButtonPressedOnYesNoPromptMenu();

    }


    public void NoButtonPressedOnYesNoPromptMenu()
    {
        GameManager.Instance.NoButtonPressedOnYesNoPromptMenu();
    }


    public void UpdateScore()
    {
        uiModel.Score = GameManager.Instance.Score;
        uiModel.ScoreText.text = uiModel.Score.ToString();

        uiModel.Highscore = GameManager.Instance.HighScore;
        uiModel.HighscoreText.text = uiModel.Highscore.ToString();
    }


    public void UpdateFinalScores()
    {
        uiModel.FinalScoreText.text = "Score: " + uiModel.Score.ToString();
        uiModel.FinalHighScoreText.text = "Highscore: " + uiModel.Highscore.ToString();
    }


    public void DisplayCurrentFireModeInHUD(int currentFireMode)
    {
        HideAllFireModesInHUD();

        switch (currentFireMode)
        {
            case 0:
                uiModel.SingleFireModeImage.enabled = true; 
                break;
            case 1:
                uiModel.BurstFireModeImage.enabled = true;
                break;
            case 2:
                uiModel.AutoFireModeImage.enabled = true;
                break;
        }
    }



    private void HideAllFireModesInHUD()
    {
        uiModel.SingleFireModeImage.enabled = false;
        uiModel.BurstFireModeImage.enabled = false;
        uiModel.AutoFireModeImage.enabled = false;
    }


    public void UpdateAmmo(int currentAmmo, int magazineSize)
    {
        uiModel.AmmoText.text = currentAmmo.ToString() + " / " + magazineSize.ToString();
    }


    public void AmmoReloading()
    {
        uiModel.AmmoText.text = "reloading";
    }


    public void UpdateDIMValue(float value)
    {
        float normalizedValue = Mathf.InverseLerp(1.5f, 10f, value);

        if (normalizedValue <= 0f)
        {
            uiModel.PlayerHealthValue = 0;
            UpdatePlayerHealth(uiModel.PlayerHealthValue);
        }

        uiModel.DimValueImage.fillAmount = normalizedValue;
    }


    public void UpdatePlayerHealth(int healthValue)
    {
        uiModel.PlayerHealth.text = healthValue.ToString();
    }


    public void PlayButtonSound()
    {
        SoundManager.Instance.Play(Sounds.BUTTON_CLICK);
    }
}
