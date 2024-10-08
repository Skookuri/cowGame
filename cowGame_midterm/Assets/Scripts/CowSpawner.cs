// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class CowSpawner: MonoBehaviour
// {
//     public int numberToSpawn;
//     public List<GameObject> spawnPool;
//     private int currentCows = 0;
//     private GameObject player;
//     // public GameTimer timer;
//     // public GameObject spawnArea;

//     void Start()
//     {
//         player = GameObject.FindWithTag("Player");
//         spawnObjects();
//     }

//     void Update()
//     {
        
//         Debug.Log(returnCurrentCows());
//     }

//     public void spawnObjects()
//     {
//         // public timer;
//         int randomItem = 0;
//         float spawnDelay = 0;
//         GameObject toSpawn;

//         float screenX, screenY;
//         Vector2 pos;
        
//         //while (timer.timerIsRunning == true)
//         //{
//             //Debug.Log("Timer is running");
//         if (currentCows < 5)
//         {
//             randomItem = Random.Range(0,spawnPool.Count);
//             toSpawn = spawnPool[randomItem];

//             screenX = Random.Range(-6, 7);
//             screenY = Random.Range(-4, 4);

//             pos = new Vector2(screenX, screenY);

//             // yield return new WaitForSeconds(2);
//             Instantiate(toSpawn, pos,toSpawn.transform.rotation);
//             currentCows++;
//         }
//         ///}
        
//     }

//     private void destroyObjects()
//     {
//         foreach(GameObject o in GameObject.FindGameObjectsWithTag("Cow"))
//         {
//             Destroy(o);
//             currentCows--;
//         }
//     }

//     private int returnCurrentCows()
//     {
//         return currentCows;
//     }
// }

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CowSpawner: MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
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

        Instantiate(toSpawn, pos,toSpawn.transform.rotation); 
        numberSpawned++;
        
        // for(int i = 0; i < numberToSpawn; i++)
        // {
        //     randomItem = Random.Range(0,spawnPool.Count);
        //     toSpawn = spawnPool[randomItem];

        //     screenX = Random.Range(-6, 7);
        //     screenY = Random.Range(-4, 4);

        //     pos = new Vector2(screenX, screenY);

        //     Instantiate(toSpawn, pos,toSpawn.transform.rotation);
        // }
    }

    private void destroyObjects()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Cow"))
        {
            Destroy(o);
        }
    }
}