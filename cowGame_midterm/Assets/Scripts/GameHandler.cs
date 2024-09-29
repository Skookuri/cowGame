using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    public int numCows = 0;
    public GameObject cowCounterText;
    private string sceneName;


    void Start(){
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        updateCowCounterText();
      }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateCowCounterText() {
        Text cowCounterTextTemp = cowCounterText.GetComponent<Text>();
        cowCounterTextTemp.text = "Cows:\n" + numCows;
    }
}
