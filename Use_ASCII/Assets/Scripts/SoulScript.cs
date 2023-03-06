using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour
{
    private Camera mainCamera;
    Rigidbody2D rb2D; // The Rigidbody component of the object

    public float dodgeSpeed = 10f; // The speed of the dodge movement
    public float dodgeDuration = 0.5f; // The duration of the dodge movement
    public float dodgeCooldown = 1f; // The cooldown between dodge movements
    
    private bool isDodging = false; // Flag indicating whether the object is currently dodging
    private float dodgeTimer = 0f; // Timer for the duration of the dodge movement
    private float dodgeCooldownTimer = 0f; // Timer for the cooldown between dodge movements

    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb2D = GetComponent<Rigidbody2D>();  //get the Rigidbody2D  off of this gameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (isDodging)
        {
            dodgeTimer += Time.deltaTime; // Increment the timer for the dodge duration
            if (dodgeTimer >= dodgeDuration) // If the dodge duration has elapsed
            {
                isDodging = false; // Set the isDodging flag to false
                dodgeTimer = 0f; // Reset the dodge timer
            }
        }
        else
        {
            dodgeCooldownTimer += Time.deltaTime; // Increment the dodge cooldown timer
            if (dodgeCooldownTimer >= dodgeCooldown) // If the dodge cooldown has elapsed
            {
                if (Input.GetKeyDown(KeyCode.W)) // If the player presses the "W" key
                {
                    Dodge(new Vector2(0f, 0f)); // Dodge forward
                }
                else if (Input.GetKeyDown(KeyCode.S)) // If the player presses the "S" key
                {
                    Dodge(new Vector2(0f, 0f)); // Dodge backward
                }
                else if (Input.GetKeyDown(KeyCode.A)) // If the player presses the "A" key
                {
                    Dodge(new Vector2(-1f, 0f)); // Dodge left
                }
                else if (Input.GetKeyDown(KeyCode.D)) // If the player presses the "D" key
                {
                    Dodge(new Vector2(1f, 0f)); // Dodge right
                }
            }
        }
        
    }

    void Dodge(Vector2 direction)
    {
        isDodging = true; // Set the isDodging flag to true
        dodgeCooldownTimer = 0f; // Reset the dodge cooldown timer
        rb2D.velocity = direction * dodgeSpeed; // Set the velocity of the Rigidbody component in the dodge direction
    }

    void OnTriggerEnter2D(Collider2D other)
        {
                if (other.gameObject.CompareTag("Player")) 
                { 
                    GameManager.instance.GetComponent<ASCIILevelLoadScript>().CatchSoul(); 
                }
        }
}
