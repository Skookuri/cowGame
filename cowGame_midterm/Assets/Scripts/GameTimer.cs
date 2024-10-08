using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public GameObject timerText;

    private void Start() {
        timerIsRunning = true;
    }

    void FixedUpdate() {
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else {
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("Level1Win");
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
