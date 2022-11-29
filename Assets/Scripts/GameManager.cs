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
    public static bool mute = false;
    private PlayerMotor motor;
    private int lastScore;

    // UI and UI fields
    public Text scoreText, coinText, modifierText, hiScoreText;
    private float score, coinScore, modifierScore;
    public GameObject gameCanvas;
    public GameObject gameOverCanvas;

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

        modifierScore = 1.0f;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString();

        //hiScoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameStarted)
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
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
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
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
