using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseInstance;

    private void Start()
    {
        
    }

    public void PauseGame()
    {
        pauseInstance.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        pauseInstance.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseInstance.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    public void nextLevel()
    {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1f;
    }
}
