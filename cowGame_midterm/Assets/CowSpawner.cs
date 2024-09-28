using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CowSpawner : MonoBehaviour {

      //Object variables
      public GameObject cowPrefab;
      public Transform[] spawnPoints;
      private int rangeEnd;
      private Transform spawnPoint;

      //Timing variables
      public float spawnRangeStart = 0.5f;
      public float spawnRangeEnd = 1.2f;
      private float timeToSpawn;
      private float spawnTimer = 0f;

      void Start(){
       //assign the length of the array to the end of the random range
             rangeEnd = spawnPoints.Length - 1 ;
       }

      void FixedUpdate(){
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                  spawnCow();
                  spawnTimer =0f;
            }
      }

      void spawnCow(){
            int SPnum = Random.Range(0, rangeEnd);
            spawnPoint = spawnPoints[SPnum];
            Instantiate(cowPrefab, spawnPoint.position, Quaternion.identity);
      }
}