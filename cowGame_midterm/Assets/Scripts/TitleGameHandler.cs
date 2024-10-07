using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGameHandler : MonoBehaviour
{
    public GameObject InstructionsImage;
    
    // Start is called before the first frame update
    void Start()
    {
        InstructionsImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Instructions() {
        InstructionsImage.SetActive(true);
    }
}
