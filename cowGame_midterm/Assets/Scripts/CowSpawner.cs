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
    private bool FaceLeft = false; // determine which way cow is facing.
    
    // Dictionary to track each cow's SpriteRenderer by GameObject
    //private Dictionary<GameObject, SpriteRenderer> cowSpriteRenderers = new Dictionary<GameObject, SpriteRenderer>();
    private SpriteRenderer spriteRenderer;


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

        Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        // Turning. Reverse if cow is moving right/left
        if ((hvMove.x < 0 && !FaceLeft) || (hvMove.x > 0 && FaceLeft)){
            cowTurn();
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

        // Get the SpriteRenderer and store it in the dictionary
        spriteRenderer = cowTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            Debug.LogError("SpriteRenderer component not found in the cow: " + spawnedCow.name);
        } /*else {
            cowSpriteRenderers[spawnedCow] = cowSpriteRenderer; // Add to dictionary
        }*/
    }

    private void destroyObjects()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Cow"))
        {
            Destroy(o);
        }

        // Clear the dictionary when all cows are destroyed
        //cowSpriteRenderers.Clear();
    }

    private void cowTurn() {
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer available to flip the cow.");
            return;
        }
        // NOTE: Switch cow facing label
        FaceLeft = !FaceLeft;

        // NOTE: Multiply cow's x local scale by -1.
        Vector3 theScale = spriteRenderer.transform.localScale;
        theScale.x *= -1;
        spriteRenderer.transform.localScale = theScale;
    }
}