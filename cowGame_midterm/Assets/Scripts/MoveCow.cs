using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour
{
    private Rigidbody2D cowRigidBody;
    private GameObject[] borders;
    private GameObject[] cactuses;
    private List<GameObject> avoidObjects = new List<GameObject>();

    private GameObject player;
    public float speed;

    // private float distance;
    // private Vector2 afraidCenter;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;

    // Start is called before the first frame update
    void Start()
    {
        borders = GameObject.FindGameObjectsWithTag("Border");
        player = GameObject.FindWithTag("Player");
        cactuses = GameObject.FindGameObjectsWithTag("Cactus");
        cowRigidBody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 centroid = beAfraid();
        Debug.Log("Centroid: " + centroid);
        if (avoidObjects.Count > 0) {
            isWalking = true;
            transform.position = Vector2.MoveTowards(this.transform.position, centroid, -1 * speed * Time.deltaTime);
            // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        } else {
            if(isWalking) {
                walkCounter -= Time.deltaTime;

                switch(walkDirection) {
                        case 0:
                            cowRigidBody.velocity = new Vector2(0, speed);
                            break;
                        case 1:
                            cowRigidBody.velocity = new Vector2(speed, 0);
                            break;
                        case 2:
                            cowRigidBody.velocity = new Vector2(0, -speed);
                            break;
                        case 3:
                            cowRigidBody.velocity = new Vector2(-speed, 0);
                            break;
                    }

                if (walkCounter < 0) {
                    isWalking = false;
                    waitCounter = waitTime;
                }
            } else {
                waitCounter -= Time.deltaTime;
                cowRigidBody.velocity = Vector2.zero;
                if (waitCounter < 0) {
                    ChooseDirection();
                }
            }
        }
        avoidObjects.Clear();
    }

    public Vector2 beAfraid()
    {
        Vector3 afraidVector = new Vector3(0, 0, 0);

        for (int i = 0; i < borders.Length; i++)
        {
            float borderDistance = Vector2.Distance(this.transform.position, borders[i].transform.position);
            if (borderDistance < 4)
            {
                avoidObjects.Add(borders[i]);
                afraidVector += borders[i].transform.position;
            }
        }

        for (int i = 0; i < cactuses.Length; i++)
        {
            float cactusDistance = Vector2.Distance(this.transform.position, cactuses[i].transform.position);
            if (cactusDistance < 1)
            {
                avoidObjects.Add(cactuses[i]);
                afraidVector += cactuses[i].transform.position;
            }
        }

        float playerDistance = Vector2.Distance(this.transform.position, player.transform.position);
        if (playerDistance < 5)
        {
            avoidObjects.Add(player);
            afraidVector += player.transform.position;
        }
        
        float numAfraid = (float)avoidObjects.Count;
        Debug.Log("Num Afraid: " + numAfraid);
        Vector2 centroid = new Vector2(0, 0);
        if (numAfraid > 0)
        {
            centroid = afraidVector / numAfraid;
        }
        return centroid;

    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}

// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class MoveCow : MonoBehaviour {

//        public float speed = 10f;
//        private float waitTime;
//        public float startWaitTime = 2f;

//        public GameObject Cow;

//        private Transform moveSpot;
//        public Rigidbody2D rb2D;
//        public float minX;
//        public float maxX;
//        public float minY;
//        public float maxY;

//        void Start(){
              
//               rb2D = transform.GetComponent<Rigidbody2D>();
//               waitTime = startWaitTime;
//               float randomX = Random.Range(minX, maxX);
//               float randomY = Random.Range(minY, maxY);
//               moveSpot = GameObject.FindGameObjectWithTag("MoveSpot").transform;
//               moveSpot.position = new Vector2(randomX, randomY) * speed;
//        }

//        void Update(){
//               transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

//               if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f){
//                      if (waitTime <= 0){
//                             float randomX = Random.Range(minX, maxX);
//                             float randomY = Random.Range(minY, maxY);
//                             moveSpot.position = new Vector2(randomX, randomY);
//                             waitTime = startWaitTime;
//                      } else {
//                             waitTime -= Time.deltaTime;
//                      }
//               }
//        }
// }

