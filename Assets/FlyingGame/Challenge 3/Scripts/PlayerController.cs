using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    public float floatForce = 3f; // Reduced float force
    private float gravityModifier = 2.5f; // Stronger gravity
    private Rigidbody playerRb;
    public bool isLowEnough = true;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public float maxHeight = 15f;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerRb.linearDamping = 1f; // Apply drag to slow float
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is low enough to float
        isLowEnough = transform.position.y < maxHeight;

        if (transform.position.y >= maxHeight)
        {
            isLowEnough = false;
            // Stop upward movement if exceeding max height
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        }

        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // Cap upward velocity (optional, smooths out float)
        if (playerRb.linearVelocity.y > 5f)
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 5f, playerRb.linearVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            // Bounce off the ground
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

        // If player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // If player collides with money, play fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}