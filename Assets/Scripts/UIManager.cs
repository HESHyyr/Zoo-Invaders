using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Text life;
    private Text score;
    private GameObject loseScene;
    private GameObject winScene;
    private GameObject pauseScene;
    private bool paused = false;
    [SerializeField] private GameObject enemyGroup;

    // Start is called before the first frame update
    void Start()
    {
        life = gameObject.transform.Find("Life").GetComponent<Text>();
        score = gameObject.transform.Find("Score").GetComponent<Text>();
        loseScene = gameObject.transform.Find("LoseScene").gameObject;
        winScene = gameObject.transform.Find("WinScene").gameObject;
        pauseScene = gameObject.transform.Find("PauseScene").gameObject;
        changeLife(LevelInfo.life);
        changeScore(LevelInfo.score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                pauseScene.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                pauseScene.SetActive(false);
            }
        }
    }

    // Change life number displayed to be the new number of life the player has
    public void changeLife(int newLife)
    {
        if (newLife <= 0)
            showLoseScene();

        life.text = string.Format("Life: {0}", newLife);
    }

    public void changeScore(int newScore)
    {
        score.text = string.Format("Score: {0}", newScore);
    }

    private void showLoseScene()
    {
        enemyGroup.SetActive(false);
        loseScene.SetActive(true);
        var ammos = GameObject.FindGameObjectsWithTag("Ammo");
        foreach (var ammo in ammos)
        {
            Destroy(ammo);
        }
        Time.timeScale = 0;
    }

    public void showWinScene(int canDo)
    {
        enemyGroup.SetActive(false);
        winScene.SetActive(true);
        var ammos = GameObject.FindGameObjectsWithTag("Ammo");
        foreach (var ammo in ammos)
        {
            Destroy(ammo);
        }
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        LevelInfo.life = 3;
        LevelInfo.score = 0;
        LevelInfo.moveCD = 1.0f;
        LevelInfo.shootCDRange = new List<float>() { 2.0f, 4.0f };
        LevelInfo.enemyHeight = 6.5f;
        SceneManager.LoadScene("MainScene");
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        LevelInfo.moveCD = LevelInfo.moveCD - 0.1f;
        LevelInfo.shootCDRange[0] = LevelInfo.shootCDRange[0] - 0.2f;
        LevelInfo.shootCDRange[1] = LevelInfo.shootCDRange[1] - 0.2f;
        LevelInfo.enemyHeight = LevelInfo.enemyHeight - 0.5f;
        LevelInfo.life++;
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseScene.SetActive(false);
    }
}
