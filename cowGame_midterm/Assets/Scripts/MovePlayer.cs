using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class PlayerMoveAround : MonoBehaviour {

      
      //public AudioSource WalkSFX;
      public AudioSource mooSound;
      private GameHandler GameHandler;
      public Rigidbody2D rb2D;
      private bool FaceLeft = false; // determine which way player is facing.
      public static float runSpeed = 5f;
      public float startSpeed = 5f;
      public bool isAlive = true;
      public bool holdingCow;
      string cowType;
      //public TextMeshProUGUI cowCounter;
      //private int count;
      public GameObject droppedCowBlack;
      public GameObject droppedCowBrown;
      public GameObject droppedCowPink;
      public Transform player;
      public Vector3 spawnOffsetCactus;

      public Image flashImage; // Assign the FlashImage in the Inspector
      public float flashDuration = 0.5f; // Duration of the flash effect

      public Vector3 spawnOffset;

      // Reference to the SpriteRenderer component in Player_Art
      private SpriteRenderer spriteRenderer;

      // Reference to the Animator component in Player_Art
      private Animator animator;


      // Sprites for the default and side views
      public Sprite defaultSprite;
      public Sprite sideSprite;
      public Sprite backSprite;
      public Sprite holdingCowSprite_black;
      public Sprite holdingCowSprite_brown;
      public Sprite holdingCowSprite_pink;
      

      void Start(){
           
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           holdingCow = false;
           cowType = "";
           GameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
           //count = 0;

           // Get the SpriteRenderer component from the Player_Art child
           spriteRenderer = transform.Find("Player_Art").GetComponent<SpriteRenderer>();

           // Get the Animator component from the Player_Art child
           animator = transform.Find("Player_Art").GetComponent<Animator>();

           // Set the default sprite initially
           spriteRenderer.sprite = defaultSprite;

           // Ensure the animator is enabled initially
           spriteRenderer.enabled = true;

            // Ensure the animator is enabled initially
            animator.enabled = true;

            if (flashImage != null) {
                  flashImage.color = new Color(1, 0, 0, 0); // Red, fully transparent
            }
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            // Update vertical movement for the Animator
            animator.SetFloat("Vertical", hvMove.y);  // Send the vertical movement to the Animator
            bool MovingHoriz = hvMove.x != 0;  // Check if horizontal movement is happening
            animator.SetBool("MovingHoriz", MovingHoriz);  // Send the horizontal movement != checker to the Animator
            animator.SetBool("HasCow", holdingCow);
            
            //Debug.Log("Horizontal: " + hvMove.x + ", Vertical: " + hvMove.y); // Log movement values
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

                 if (holdingCow) {
                        bool isBlack = false;
                        bool isBrown = false;
                        bool isPink = false;

                        if (cowType == "Cow1(Clone)") {
                              isBlack = true;
                        } else if (cowType == "Cow2(Clone)") {
                              isBrown = true;
                        } else if (cowType == "Cow3(Clone)") {
                              isPink = true;
                        }

                        Debug.Log("Setting Animator Bools: isBlack = " + isBlack + ", isBrown = " + isBrown + ", isPink = " + isPink);
                        animator.SetBool("isBlack", isBlack);   // SEND BOOL INFO TO ANIMATOR
                        animator.SetBool("isBrown", isBrown);   // SEND BOOL INFO TO ANIMATOR
                        animator.SetBool("isPink", isPink);     // SEND BOOL INFO TO ANIMATOR

                        if (hvMove.y < 0) {
                                animator.enabled = true; //Enable Animator for front view w cow
                        } else if (hvMove.y > 0) {
                                animator.enabled = true; //Enable Animator for back view w cow
                        } else if (hvMove.x != 0){
                                animator.enabled = true; //Enable Animator for side view w cow
                        } else {
                                animator.enabled = false;  // Disable Animator
                                if (isBlack) {
                                    spriteRenderer.sprite = holdingCowSprite_black; //Show non-moving default sprite
                                } else if (isBrown) {
                                    spriteRenderer.sprite = holdingCowSprite_brown; //Show non-moving default sprite
                                } else if (isPink) {
                                    spriteRenderer.sprite = holdingCowSprite_pink; //Show non-moving default sprite
                                }
                                
                                
                        }
                        
                        
                  } else if (!holdingCow) {
                        if (hvMove.y < 0) {
                              animator.enabled = true; // Enable Animator for front view
                        } else if (hvMove.y > 0) {
                              animator.enabled = true; // Enable Animator for back view
                        } else if (hvMove.x != 0) {
                              animator.enabled = true; // Enable Animator for side view
                              //spriteRenderer.sprite = sideSprite;
                              //animator.enabled = false;
                        } else {
                              animator.enabled = false; // Disable Animator
                              spriteRenderer.sprite = defaultSprite; //Show non-moving default sprite
                        }
                  }
                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x < 0 && !FaceLeft) || (hvMove.x > 0 && FaceLeft)){
                        playerTurn();
                  }
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
                  cowType = collision.gameObject.name;
                  Destroy(collision.gameObject);
                  holdingCow = true;
                  mooSound.Play();
            }
            if (collision.gameObject.CompareTag("Pen") && holdingCow) {
                  //kachingSound.Play();
                  holdingCow = false;
                  int cowValue = 0;

                  ContactPoint2D contact = collision.contacts[0];
                  Vector3 spawnPosition = new Vector3(contact.point.x, contact.point.y, 0) + spawnOffset;
                  GameObject spawnedCow = null;
                  bool isBlack = false;
                  bool isBrown = false;
                  bool isPink = false;

                  Transform cowTransform = null;

                  if (cowType == "Cow1(Clone)") {
                        cowValue = 1;
                        GameHandler.updateCowCounter(cowValue);
                        spawnedCow = Instantiate(droppedCowBlack, spawnPosition, Quaternion.identity);
                        isBlack = true;
                        cowTransform = spawnedCow.transform.Find("cow1"); //get cow1 info for Black Cow
                  }
                  if (cowType == "Cow2(Clone)") {
                        cowValue = 2;
                        GameHandler.updateCowCounter(cowValue);
                        spawnedCow = Instantiate(droppedCowBrown, spawnPosition, Quaternion.identity);
                        isBrown = true;
                        cowTransform = spawnedCow.transform.Find("cow2"); //get cow1 info for Brown Cow
                  }
                  if (cowType == "Cow3(Clone)") {
                        cowValue = 4;
                        GameHandler.updateCowCounter(cowValue);
                        spawnedCow = Instantiate(droppedCowPink, spawnPosition, Quaternion.identity);
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

                  //count = count + 1;
                  //cowCounter.text = "Cows: " + count.ToString();
            }
            if (collision.gameObject.CompareTag("Cactus") && holdingCow) {
                  holdingCow = false;

                  StartCoroutine(FlashScreen());

                  float x = player.transform.position.x - 1;
                  float y = player.transform.position.y;
                  float z = player.transform.position.y;
                  ContactPoint2D contact = collision.contacts[0];
                  Vector3 spawnPosition = new Vector3(contact.point.x, contact.point.y, 0) + spawnOffsetCactus;

                  GameObject spawnedCow = null; 
                  Transform cowTransform = null;

                  bool isBlack = false;
                  bool isBrown = false;
                  bool isPink = false;

                  if (cowType == "Cow1(Clone)") {
                        spawnedCow = Instantiate(droppedCowBlack, spawnPosition, Quaternion.identity);
                        isBlack = true;
                        cowTransform = spawnedCow.transform.Find("cow1"); //get cow1 info for Black Cow
                  }
                  if (cowType == "Cow2(Clone)") {
                        spawnedCow = Instantiate(droppedCowBrown, spawnPosition, Quaternion.identity);
                        isBrown = true;
                        cowTransform = spawnedCow.transform.Find("cow2"); //get cow1 info for Brown Cow
                  }
                  if (cowType == "Cow3(Clone)") {
                        spawnedCow = Instantiate(droppedCowPink, spawnPosition, Quaternion.identity);
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
      }

      private IEnumerator FlashScreen()
      {
            float elapsedTime = 0f;
            float targetAlpha = 1f;
            Color color = flashImage.color;

            while (elapsedTime < flashDuration)
            {
                  float alpha = Mathf.Lerp(0, targetAlpha, elapsedTime / flashDuration);
                  flashImage.color = new Color(color.r, color.g, color.b, alpha);
                  elapsedTime += Time.deltaTime;
                  yield return null;
            }
    
            // Ensure it's fully opaque
            flashImage.color = new Color(color.r, color.g, color.b, targetAlpha);

            // Pause at full opacity
            //yield return new WaitForSeconds(0.1f); // Short pause before fading out

            // Fade out
            elapsedTime = 0f;

            while (elapsedTime < flashDuration)
            {
                  float alpha = Mathf.Lerp(targetAlpha, 0, elapsedTime / flashDuration);
                  flashImage.color = new Color(color.r, color.g, color.b, alpha);
                  elapsedTime += Time.deltaTime;
                  yield return null;
            }

            // Ensure it's fully transparent
            flashImage.color = new Color(color.r, color.g, color.b, 0);
      }
}