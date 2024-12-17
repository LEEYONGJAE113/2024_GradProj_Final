using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("Select Stage");
    }
    public void Stage0()
    {
        SceneManager.LoadScene("Stage 0", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GameQuit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.GameStart();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
