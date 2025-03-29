using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.UIElements;
using UnityEngine.SceneManagement;
public class GameManager1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager1 Instance { get; private set; }
    public float obstacleSpeed = 23f;
    public GameObject[] obstaclePrefabs;
    public Transform player;
    public float destroyDistance = -10f;
    public TextMeshProUGUI ScoreText;
    private bool flag = true;
    public static int HighScore = 0;
    public static int Score = 0;
    public Button scorebutton;
    public GameObject gameOverScreen;
    private bool isGameOver = false;

    public GameObject _test;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        HighScore= PlayerPrefs.GetInt("HighScore",0);
    }
    void Start()
    {
        if (GameManager1.Instance.obstaclePrefabs.Length != 4)
        {
            Debug.LogError("Please assign exactly 4 prefabs to the Obstacle Prefabs array!");
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); // Ensure it starts hidden
        }
    }

    public void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.z < -10)
            {
                Destroy(obstacle);
            }
        }
    }

    public void AddScore()
    {
        Score += 1;
        if (HighScore < Score)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        scorebutton.onClick.AddListener(ButtonClick);

        if (flag)
        {
            ScoreText.text = "Score: " + Score;
        }
        else
        {
            ScoreText.text = "High Score: " + HighScore;
        }

    }

    public void ButtonClick()
    {

        if (flag && scorebutton != null)
        {
            flag = false;
        }
        else
        {
            flag = true;
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
                RenderSettings.fog = false;
            }
            else
            {
                Debug.Log("GameOverScreen is null or destroyed!");
            }

        }


    }

    public void Restart()
    {
        Debug.Log("Restarting Game");
        Time.timeScale = 1f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
            RenderSettings.fog = true;
        }
        isGameOver = false;
        ScoreText.text = "Score: 0";
        Score = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name)
        SceneManager.LoadScene(0);


    }


}