using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleHandler : MonoBehaviour
{
    public GameObject InstructionsUI;
    
    // Start is called before the first frame update
    void Start()
    {
        InstructionsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            InstructionsUI.SetActive(false);
        }
    }

    public void Instructions() {
        InstructionsUI.SetActive(true);
    }

    public void Play() {
        Debug.Log("hi");
        SceneManager.LoadScene("Level1");
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}