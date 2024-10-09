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
    private int money;

    //pause menu
    public GameObject pauseUI;
    public GameObject instructionsUI;
    bool isPaused;


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

    public void updateCowCounter() {
        money = money + 1;
        cowCounter.text = "$ Earned: $" + money.ToString() + "k";
    }

    public void Pause() {
        //need to add stuff that changes the time
        isPaused = true;
        pauseUI.SetActive(true);
    }

    public void Resume() {
        //need to add time stuff
        isPaused = false;
        pauseUI.SetActive(false);
    }

    public void Instructions() {
        instructionsUI.SetActive(true);
    }

    public void QuitToTitle() {
        SceneManager.LoadScene("TitleScene");
    }
}
