using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlatform : Audio
{
    private Rigidbody2D rb;
    public Animator AnimatorPlatform;
    public GameObject Self;
    public GameObject SpawnPoint;
    public bool PlayerOn = false;
    public bool isGrounded = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player" || collision.tag == "PlayerPart")
    //    {
    //        rb.velocity = Vector2.up * -1.2f;
    //    }
    //    else
    //    {
    //        rb.velocity = Vector2.up * 0;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AnimatorPlatform.Play("BubblePlatformExplode");
            PlayerOn = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PlayerPart")
        {
            PlayerOn = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (PlayerOn == true)
        {
            rb.velocity = Vector2.up * -1.2f;
        }
        else
        {
            rb.velocity = Vector2.up * 0;
        }

        if (isGrounded == true)
        {
            isGrounded = false;
            AnimatorPlatform.Play("BubblePlatformExplode");
        }
     }

        void Respawn()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGrounded = false;
        Self.transform.position = SpawnPoint.transform.position;
        AnimatorPlatform.Play("BubblePlatformRespawn");
        PlayerOn = false;
    }
    public void PlayIt()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
    }
}
