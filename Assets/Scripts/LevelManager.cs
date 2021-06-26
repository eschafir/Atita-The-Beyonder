using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;

    MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
            musicManager.GetComponent<AudioSource>().Pause();
        }

        else
        {
            Time.timeScale = 1.0f;
            musicManager.GetComponent<AudioSource>().Play();
            pausePanel.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
