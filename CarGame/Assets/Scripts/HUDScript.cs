using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI finishedTime;
    [SerializeField] Image fuelSlider;
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject endScreenUI;

    CarMovement carMovement; // Reference to the CarMovement script

    public void Start()
    {
        carMovement = Object.FindFirstObjectByType<CarMovement>();
        pauseMenuUI.SetActive(false);
    }


    void Update()
    {
        if (carMovement != null)
        {
            // Update time text
            UpdateTimeText();

            // Check if the player collected 10 coins
            if (carMovement.coinCount >= 10)
            {
                Debug.Log("You Win");
                EndGame();
                finishedTime.text = carMovement.winTime.ToString("F2");
            } 
        }
        coinText.text = "Coins : " + carMovement.coinCount.ToString() + " / 10";
        fuelSlider.fillAmount = carMovement.nitroFuel / 100.0f;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            // Update time text
            timeText.text = "Time : " + Time.timeSinceLevelLoad.ToString("F2");
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

    }
    public void HomeScreen()
    {
        // Load the home screen scene
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("HomeScreen");
    }

    public void EndGame()
    {
        endScreenUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}