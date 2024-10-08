using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour
{
    private Rigidbody2D cowRigidBody;

    private GameObject player;
    public float speed;

    private float distance;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;
    //public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // target = GameObject.FindWithTag("Player").transform;
        cowRigidBody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 direction = transform.position - target.position;

        // if(direction.sqrMagnitude < 25f)
        // { 
        //     transform.Translate(direction.normalized * Time.deltaTime, Space.World);
        //     transform.forward = direction.normalized;

        if (distance < 5)
        {
            isWalking = true;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            if(isWalking)
            {
                walkCounter -= Time.deltaTime;

                switch(walkDirection)
                    {
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

                if (walkCounter < 0)
                {
                    isWalking = false;
                    waitCounter = waitTime;
                }
            }
            else
            {
                waitCounter -= Time.deltaTime;
                cowRigidBody.velocity = Vector2.zero;
                if (waitCounter < 0)
                {
                    ChooseDirection();
                }
        }
        

        }
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    //   {
    //         if (collision.gameObject.CompareTag("Cow")) {
    //               Destroy(GameObject.FindGameObjectWithTag("Cow"));
    //               holdingCow = true;
    //         }
    //         if (collision.gameObject.CompareTag("Pen") && holdingCow) {
    //               holdingCow = false;
    //         }
    //   }
}
