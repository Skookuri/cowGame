using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    //cow slash money counter
    public TextMeshProUGUI cowCounter;
    public int money;

    //pause menu
    public GameObject pauseUI;
    public GameObject instructionsUI;
    bool isPaused;

    //timer
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public GameObject timerText;


    //tbh dont know why these exist
    //private GameObject player;
    //private string sceneName;
    
    void Start(){
        //player = GameObject.FindWithTag("Player");
        //sceneName = SceneManager.GetActiveScene().name;
        money = 0;
        isPaused = false;
        pauseUI.SetActive(false);
        instructionsUI.SetActive(false);
        timerIsRunning = true;
      }

    // Update is called once per frame
    void Update()
    {
        //for pause menu
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }

        //if this is in update, resumes right after pausing because still 
        //getting escape input
        //if (isPaused && Input.GetKeyDown(KeyCode.Escape)) {
        //    Resume();
        //}

        //for hiding instructions
        if (Input.GetMouseButtonDown(0)) {
            instructionsUI.SetActive(false);
        }
    }

    public void updateCowCounter(int cowValue) {
        money = money + cowValue;
        cowCounter.text = "$ Earned: $" + money.ToString() + "k";
    }

    public void Pause() {
        isPaused = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void Resume() {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void Instructions() {
        instructionsUI.SetActive(true);
    }

    public void QuitToTitle() {
        SceneManager.LoadScene("TitleScene");
    }


    //timer stuff
    void FixedUpdate() {
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else {
                timeRemaining = 0;
                timerIsRunning = false;
                if (money >= 20) {
                    SceneManager.LoadScene("Level1Win");
                }
                else {
                    SceneManager.LoadScene("Level1Loss");
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Text timeTextTemp = timerText.GetComponent<Text>();
        timeTextTemp.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
