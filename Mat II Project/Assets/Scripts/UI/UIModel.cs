using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIModel : MonoBehaviour
{
    [SerializeField] private GameObject titleTextGameObject;
    public GameObject TitleTextGameObject { get => titleTextGameObject; set => titleTextGameObject = value; }


    [SerializeField] private GameObject mainMenuUIGameObject;
    public GameObject MainMenuUIGameObject { get => mainMenuUIGameObject; set => mainMenuUIGameObject = value; }


    [SerializeField] private GameObject pauseMenuUIGameObject;
    public GameObject PauseMenuUIGameObject { get => pauseMenuUIGameObject; set => pauseMenuUIGameObject = value;}


    [SerializeField] private GameObject restartMenuUIGameObject;
    public GameObject RestartMenuUIGameObject { get => restartMenuUIGameObject; set => restartMenuUIGameObject = value;}


    [SerializeField] private GameObject hudMenuUIGameObject;
    public GameObject HUDMenuUIGameObject { get => hudMenuUIGameObject; set => hudMenuUIGameObject = value;}


    [SerializeField] private GameObject yesNoPromptGameObject;
    public GameObject YesNoPromptGameObject { get => yesNoPromptGameObject; set => yesNoPromptGameObject = value;}


    [SerializeField] private Image singleFireModeImage;
    public Image SingleFireModeImage { get => singleFireModeImage; set => singleFireModeImage = value; }


    [SerializeField] private Image burstFireModeImage;
    public Image BurstFireModeImage {  get => burstFireModeImage; set => burstFireModeImage = value; }


    [SerializeField] private Image autoFireModeImage;
    public Image AutoFireModeImage { get => autoFireModeImage; set => autoFireModeImage = value; }


    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }


    [SerializeField] private TextMeshProUGUI highscoreText;
    public TextMeshProUGUI HighscoreText { get => highscoreText; set => highscoreText = value; }


    [SerializeField] private TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI FinalScoreText { get => finalScoreText; set => finalScoreText = value; }


    [SerializeField] private TextMeshProUGUI finalHighScoreText;
    public TextMeshProUGUI FinalHighScoreText { get => finalHighScoreText; set => finalHighScoreText = value; }


    [SerializeField] private TextMeshProUGUI ammoText;
    public TextMeshProUGUI AmmoText { get => ammoText; set => ammoText = value; }


    [SerializeField] private TextMeshProUGUI playerHealth;
    public TextMeshProUGUI PlayerHealth { get => playerHealth; set => playerHealth = value; }


    [SerializeField] private Image dimValueImage;
    public Image DimValueImage { get => dimValueImage; set => dimValueImage = value; }


    private int score;
    public int Score { get => score; set => score = value; }


    private int highscore;
    public int Highscore { get => highscore; set => highscore = value; }


    private int playerHealthValue;
    public int PlayerHealthValue { get => playerHealthValue; set => playerHealthValue = value; }
}











