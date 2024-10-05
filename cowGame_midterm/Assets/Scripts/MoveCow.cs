using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCow : MonoBehaviour
{
    private GameObject player;
    public float speed;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < 5)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            Debug.Log("Player position: " + player.transform.position);
            // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        
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
