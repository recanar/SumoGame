using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer;
    public float score;
    public float remainSumo;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI remainSumoText;
    public float PlayerCount { get; set; }
    public static GameManager Instance;
    [SerializeField] private GameObject pausePanel;
    public GameObject[] sumos;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        sumos=GameObject.FindGameObjectsWithTag("Sumo");
        remainSumo = sumos.Length;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
    }

    private void TimerUpdate()//reduce time 1 per second and show in user ui
    {
        timer -= Time.deltaTime;
        timerText.text = ((int)timer).ToString();
        if (timer <= 0)
        {
            GameOver();
        }
    }
    public void AddScore(int addScore)//change score when eliminate an enemy or by getting collectibles
    {
        score += addScore;//add score as parameter when function called
        scoreText.text = score.ToString();
    }

    public void ReduceSumoCount()
    {
        remainSumo--;
        remainSumoText.text = remainSumo.ToString();
        if (remainSumo==1)
        {
            GameOver();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
