using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager singleton;

    public static bool gameOver;
    public static bool isGameStarted = false;
    public static bool mute, pause = false;
    private PlayerMotor motor;
    private int lastScore;

    // UI and UI fields
    public Text scoreText, coinText, modifierText, hiScoreText, saveCoin, gameOverScore, gameOverCoin, soundsText;
    private float score, coinScore, modifierScore;
    public GameObject gameCanvas, pauseButton;
    public GameObject gameOverCanvas;

    //Sound
    private AudioManager audioManager;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            mute = false;
            soundsText.text = "";
        }
        else
        {
            mute = true;
            soundsText.text = "/";
        }

        modifierScore = 1.0f;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        audioManager = FindObjectOfType<AudioManager>();
        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString();

        saveCoin.text = PlayerPrefs.GetInt("Coin", 0).ToString();

        hiScoreText.text = PlayerPrefs.GetInt("Hiscore", 0).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameStarted && !pause)
        {
            // Bump the score up
            lastScore = (int)score;
            score += (Time.deltaTime * modifierScore);
            if (lastScore == (int)score)
            {
                scoreText.text = score.ToString("0");
            }
        }
        
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
        audioManager.Play("Coin");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPause(bool onPause)
    {
        pause = onPause;
        if (!onPause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void OnRestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }

    public void OnPlay()
    {
        isGameStarted = true;
        motor.StartRunning();
    }

    public void OnDeath()
    {
        isGameStarted = false;
        gameOver = true;
        gameOverScore.text = score.ToString("0");
        gameOverCoin.text = coinScore.ToString("0");
        gameCanvas.SetActive(false);
        pauseButton.SetActive(false);
        gameOverCanvas.SetActive(true);
        audioManager.Play("GameOver");

        // Check if this is a highscore
        if (score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }

        // Save Coin
        float coin = PlayerPrefs.GetInt("Coin");
        coin += coinScore;
        PlayerPrefs.SetInt("Coin", (int)coin);
    }

    //Mute
    public void ToggleMute()
    {
        if (mute)
        {
            mute = false;
            soundsText.text = "";
            PlayerPrefs.SetInt("Mute", 0);
        }
        else
        {
            mute = true;
            soundsText.text = "/";
            PlayerPrefs.SetInt("Mute", 1);
        }
    }

    //Quit Game
    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Quit Game");
    }
}
