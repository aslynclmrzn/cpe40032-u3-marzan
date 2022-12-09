using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem forceParticle;
    public ParticleSystem splashParticle;
    public AudioClip ouchSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;

    void Start()
    {
     
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            forceParticle.Stop();
            splashParticle.Play();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
          
            isOnGround = true;
            forceParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            forceParticle.Stop();
            playerAudio.PlayOneShot(ouchSound, 1.0f);
           
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            Score.scoreNum += 1;
        }
    }
}