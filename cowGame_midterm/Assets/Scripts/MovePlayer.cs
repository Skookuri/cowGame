using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour {

      //public Animator anim;
      //public AudioSource WalkSFX;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
      public bool holdingCow;

      void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           holdingCow = false;
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  Debug.Log("Old position" + transform.position);
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;
                  Debug.Log("New position" + transform.position);

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                  //     anim.SetBool ("Walk", true);
                  //     if (!WalkSFX.isPlaying){
                  //           WalkSFX.Play();
                  //     }
                  } else {
                  //     anim.SetBool ("Walk", false);
                  //     WalkSFX.Stop();
                 }

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        playerTurn();
                  }
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            //Vector3 theScale = transform.localScale;
            //theScale.x *= -1;
            //transform.localScale = theScale;
      }

      private void OnCollisionEnter2D(Collision2D collision)
      {
            if (collision.gameObject.CompareTag("Cow") && !holdingCow) {
                  holdingCow = true;
                  Destroy(collision.gameObject);
            } else if (collision.gameObject.CompareTag("Pen") && holdingCow) {
                  holdingCow = false;
            }
      }
      
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MoveSprite : MonoBehaviour
// {
//     // Speed at which the sprite moves
//     public float moveSpeed = 1f;

//     // Variable to store the initial scale of the sprite
//     private Vector3 originalScale;

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Save the original scale of the sprite at the start
//         originalScale = transform.localScale;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Check if the "A" key is held down
//         if (Input.GetKey(KeyCode.A))
//         {
//             // Move the sprite exactly by 1 unit per frame in the left direction
//             transform.Translate(Vector3.left * moveSpeed);

//             // Flip the sprite by setting the localScale's x to negative
//             transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
//         }

//         // Check if the "D" key is held down
//         if (Input.GetKey(KeyCode.D))
//         {
//             // Move the sprite exactly by 1 unit per frame in the right direction
//             transform.Translate(Vector3.right * moveSpeed);

//             // Restore the sprite's original scale to face right
//             transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
//         }
//     }
// } //comment
