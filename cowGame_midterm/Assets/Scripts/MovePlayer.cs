using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    // Speed at which the sprite moves
    public float moveSpeed = 1f;

    // Variable to store the initial scale of the sprite
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        // Save the original scale of the sprite at the start
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "A" key is held down
        if (Input.GetKey(KeyCode.A))
        {
            // Move the sprite exactly by 1 unit per frame in the left direction
            transform.Translate(Vector3.left * moveSpeed);

            // Flip the sprite by setting the localScale's x to negative
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Check if the "D" key is held down
        if (Input.GetKey(KeyCode.D))
        {
            // Move the sprite exactly by 1 unit per frame in the right direction
            transform.Translate(Vector3.right * moveSpeed);

            // Restore the sprite's original scale to face right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}
