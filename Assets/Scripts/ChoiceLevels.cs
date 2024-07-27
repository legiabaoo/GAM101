using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceLevels : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1f;
    }
}
