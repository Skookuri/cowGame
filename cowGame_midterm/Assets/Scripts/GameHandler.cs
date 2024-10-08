using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private GameObject player;
    private string sceneName;
    public TextMeshProUGUI cowCounter;
    private int count;
    


    void Start(){
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        count = 0;
      }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCowCounter() {
        Debug.Log("hi");
        count = count + 1;
        cowCounter.text = "$ Earned: $" + count.ToString() + "k";
    }
}
