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
        changeLife(3);
        changeScore(0);
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
        winScene.SetActive(true);
        var ammos = GameObject.FindGameObjectsWithTag("Ammo");
        foreach (var ammo in ammos)
        {
            Destroy(ammo);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
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
