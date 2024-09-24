using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] private UIModel uiModel;


    public void ShowMainMenuUI()
    {
        HideAllUI();
        uiModel.TitleTextGameObject.SetActive(true);
        uiModel.MainMenuUIGameObject.SetActive(true);
    }


    public void ShowPauseMenuUI()
    {
        HideAllUI();
        uiModel.PauseMenuUIGameObject.SetActive(true);
    }


    public void ShowRestartMenuUI()
    {
        HideAllUI();
        uiModel.RestartMenuUIGameObject.SetActive(true);
    }


    public void ShowHUDMenuUI()
    {
        HideAllUI();
        uiModel.HUDMenuUIGameObject.SetActive(true);
    }


    public void ShowYesNoPromptUI()
    {
        HideAllUI();
        uiModel.YesNoPromptGameObject.SetActive(true);
    }


    public void HideAllUI()
    {
        uiModel.TitleTextGameObject.SetActive(false);
        uiModel.MainMenuUIGameObject.SetActive(false);
        uiModel.PauseMenuUIGameObject.SetActive(false);
        uiModel.RestartMenuUIGameObject.SetActive(false);
        uiModel.YesNoPromptGameObject.SetActive(false);
        uiModel.HUDMenuUIGameObject.SetActive(false);
    }
}












