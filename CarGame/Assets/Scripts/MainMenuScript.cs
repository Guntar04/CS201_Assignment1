using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        HUDScript.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        HUDScript.isPaused = false;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        //set paused to false
        
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
