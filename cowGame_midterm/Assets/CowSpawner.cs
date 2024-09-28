using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CowSpawner: MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    // public GameObject spawnArea;

    void Start()
    {
        spawnObjects();

    }

    public void spawnObjects()
    {
        int randomItem = 0;
        float spawnDelay = 0;
        GameObject toSpawn;
        // MeshCollider c = spawnArea.GetComponent<MeshCollider>();
        // Debug.Log("Min x: " + c.bounds.min.x);
        // Debug.Log("Max x: " + c.bounds.max.x);
        // Debug.Log("Min y: " + c.bounds.min.y);
        // Debug.Log("Max y: " + c.bounds.max.y);

        float screenX, screenY;
        Vector2 pos;

        for(int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0,spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            screenX = Random.Range(-10, 2);
            screenY = Random.Range(-2, 6);

            // screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            // Debug.Log("screenX: " + screenX);
            // screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            // Debug.Log("screenY: " + screenY);

            pos = new Vector2(screenX, screenY);

            Instantiate(toSpawn, pos,toSpawn.transform.rotation);
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

// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class CowSpawner : MonoBehaviour {

//       //Object variables
//       public GameObject cowPrefab;
//       public Transform[] spawnPoints;
//       private int rangeEnd;
//       private Transform spawnPoint;

//       //Timing variables
//       public float spawnRangeStart = 0.5f;
//       public float spawnRangeEnd = 5f;
//       private float timeToSpawn;
//       private float spawnTimer = 0f;

//       void Start(){
//        //assign the length of the array to the end of the random range
//              rangeEnd = spawnPoints.Length - 1 ;
//        }

//       void FixedUpdate(){
//             timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
//             spawnTimer += 0.01f;
//             if (spawnTimer >= timeToSpawn){
//                   spawnCow();
//                   spawnTimer =0f;
//             }
//       }

//       void spawnCow(){
//             int SPnum = Random.Range(0, rangeEnd);
//             spawnPoint = spawnPoints[SPnum];
//             Instantiate(cowPrefab, spawnPoint.position, Quaternion.identity);
//       }
// }