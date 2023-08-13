using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    public float gravitiyModifier;

    public bool isOnGround = true;

    private Vector3 orginal;

    public bool gameOver = false;

    private Animator anim;

    public GameObject CanvasUI;

    public ParticleSystem olumIzi;
    public ParticleSystem ayakIzi;

    gameManeger gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        orginal = Physics.gravity;
        Physics.gravity *= gravitiyModifier;

        gameManager = GameObject.FindObjectOfType<gameManeger>();
    }

    void ResetGravity()
    {
        Physics.gravity = orginal;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            anim.SetTrigger("Jump_trig");
            ayakIzi.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            ayakIzi.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("OYUN BÝTTÝ");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            CanvasUI.SetActive(true);
            ResetGravity();
            olumIzi.Play();
            ayakIzi.Stop();

        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            gameManager.AddScore(10);
            Destroy(collision.gameObject);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }
}


    
