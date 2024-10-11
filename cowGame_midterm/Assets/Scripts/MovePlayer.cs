using System.Collections.Generic;
using System.Collections;
using UnityEngine;
//using TMPro;

public class PlayerMoveAround : MonoBehaviour {

      //public Animator anim;
      //public AudioSource WalkSFX;
      private GameHandler GameHandler;
      public Rigidbody2D rb2D;
      private bool FaceLeft = false; // determine which way player is facing.
      public static float runSpeed = 5f;
      public float startSpeed = 5f;
      public bool isAlive = true;
      public bool holdingCow;
      //public TextMeshProUGUI cowCounter;
      //private int count;
      public GameObject droppedCow;
      public Transform player;


      public GameObject cowInPen; // The sprite prefab to instantiate
      public Vector3 spawnOffset;

      // Reference to the SpriteRenderer component in Player_Art
      private SpriteRenderer spriteRenderer;


      // Sprites for the default and side views
      public Sprite defaultSprite;
      public Sprite sideSprite;
      public Sprite backSprite;
      public Sprite holdingCowSprite;
      

      void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           holdingCow = false;
           GameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
           //count = 0;

           // Get the SpriteRenderer component from the Player_Art child
           spriteRenderer = transform.Find("Player_Art").GetComponent<SpriteRenderer>();


           // Set the default sprite initially
           spriteRenderer.sprite = defaultSprite;
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  //transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;
                  float moveHorizontal = Input.GetAxis("Horizontal");
                  float moveVertical = Input.GetAxis("Vertical");

                  Vector2 movement = new Vector2(moveHorizontal, moveVertical) * runSpeed;
                  rb2D.velocity = movement;

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                  //     anim.SetBool ("Walk", true);
                  //     if (!WalkSFX.isPlaying){
                  //           WalkSFX.Play();
                  //     }
                  } else {
                  //     anim.SetBool ("Walk", false);
                  //     WalkSFX.Stop();
                 }

                 if (hvMove.x != 0) {
                        spriteRenderer.sprite = sideSprite;
                 } else if (hvMove.y > 0) {
                        spriteRenderer.sprite = backSprite;
                 } else {
                        spriteRenderer.sprite = defaultSprite; // Reset to default when not moving sideways
                 }
                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x < 0 && !FaceLeft) || (hvMove.x > 0 && FaceLeft)){
                        playerTurn();
                  }
            }

            if (holdingCow) {
                  spriteRenderer.sprite = holdingCowSprite;
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceLeft = !FaceLeft;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = spriteRenderer.transform.localScale;
            theScale.x *= -1;
            spriteRenderer.transform.localScale = theScale;
      }

      private void OnCollisionEnter2D(Collision2D collision)
      {
            if (collision.gameObject.CompareTag("Cow") && !holdingCow) {
                  
                  Destroy(collision.gameObject);
                  holdingCow = true;
            }
            if (collision.gameObject.CompareTag("Pen") && holdingCow) {
                  holdingCow = false;
                  GameHandler.updateCowCounter();

                  ContactPoint2D contact = collision.contacts[0];
                  Vector3 spawnPosition = new Vector3(contact.point.x, contact.point.y, 0) + spawnOffset;
                  Instantiate(cowInPen, spawnPosition, Quaternion.identity);

                  //count = count + 1;
                  //cowCounter.text = "Cows: " + count.ToString();
            }
            if (collision.gameObject.CompareTag("Cactus") && holdingCow) {
                  holdingCow = false;
                  float x = player.transform.position.x - 1;
                  float y = player.transform.position.y;
                  float z = player.transform.position.y;
                  Instantiate(droppedCow, new Vector3(x, y, z), Quaternion.identity);
            }
      }
}