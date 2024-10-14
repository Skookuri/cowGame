using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CowSpawner: MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool = new List<GameObject>();
    // public GameObject spawnArea;
    // public GameHandler timer;
    private float waitCounter;
    public float waitTime;
    private bool timerIsRunning;
    private int numberSpawned;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        waitCounter = waitTime;
        spawnObjects();

    }

    void Update()
    {
        waitCounter -= Time.deltaTime;

        // Debug.Log("Wait time: " + waitTime);
        // Debug.Log("Wait Counter: " + waitCounter);
        if (waitCounter <= 0)
        {
            spawnObjects();
            waitCounter = waitTime;
        }
        // timerIsRunning = timer.GetComponent<timerIsRunning>();
        // while (timerIsRunning)
        // {
        //     spawnObjects();
        // }
    }

    public void spawnObjects()
    {
        int randomItem = 0;
        GameObject toSpawn;
        // MeshCollider c = spawnArea.GetComponent<MeshCollider>();
        // Debug.Log("Min x: " + c.bounds.min.x);
        // Debug.Log("Max x: " + c.bounds.max.x);
        // Debug.Log("Min y: " + c.bounds.min.y);
        // Debug.Log("Max y: " + c.bounds.max.y);

        float screenX, screenY;
        Vector2 pos;

        randomItem = Random.Range(0,spawnPool.Count);
        toSpawn = spawnPool[randomItem];

        screenX = Random.Range(-6, 7);
        screenY = Random.Range(-4, 4);

        pos = new Vector2(screenX, screenY);

        GameObject spawnedCow = Instantiate(toSpawn, pos,toSpawn.transform.rotation); 
        numberSpawned++;

        bool isBlack = false;
        bool isBrown = false;
        bool isPink = false;

        Transform cowTransform = null;

        if (spawnedCow.name == "Cow 1(Clone)") {
            isBlack = true;
            cowTransform = spawnedCow.transform.Find("cow1"); //get cow1 info for Black Cow
        } else if (spawnedCow.name == "Cow2(Clone)"){
            isBrown = true;
            cowTransform = spawnedCow.transform.Find("cow2"); //get cow2 info for Brown Cow
        } else if (spawnedCow.name == "Cow3(Clone)") {
            isPink = true;
            cowTransform = spawnedCow.transform.Find("cow3"); //get cow3 info for Pink Cow
        }

        Animator cowAnimator = cowTransform.gameObject.AddComponent<Animator>(); //added Animator component to cow1/cow2/cow3

        //set Cow_Controller as Animator Controller
        RuntimeAnimatorController cowController = Resources.Load<RuntimeAnimatorController>("Cow_Controller");

        if (cowController == null) {
            Debug.LogError("Cow_Controller could not be found in the Resources folder!");
        } else if (cowTransform != null) {
            cowAnimator.runtimeAnimatorController = cowController;
            // Set the bool values for the Animator
            cowAnimator.SetBool("isBlack", isBlack);   // SEND BOOL INFO TO ANIMATOR
            cowAnimator.SetBool("isBrown", isBrown);   // SEND BOOL INFO TO ANIMATOR
            cowAnimator.SetBool("isPink", isPink);     // SEND BOOL INFO TO ANIMATOR
        } else {
            Debug.LogError("Cow transform not found for the spawned cow: " + spawnedCow.name);
        }
    }

    private void destroyObjects()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Cow"))
        {
            Destroy(o);
        }
    }
}