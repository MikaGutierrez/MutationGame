using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //For Movement
    public float speed;
    private float moveInput;

    //For Jump
    public bool isJumping;
    public float jumpForce;
    public float jumpTimeCounter;
    public float jumpTime;

    //Ground Check
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Rigidbody2D
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            //if (moveInput > 0)
            //{
            //    transform.Rotate(Vector3.forward, -rotateSpeed);
            //}
            //else if (moveInput < 0)
            //{
            //    transform.Rotate(Vector3.forward, rotateSpeed);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            //Char_Animator.SetTrigger("Jump");
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else isJumping = false;

        }


        if (Input.GetKeyUp(KeyCode.Space) )
        {
            isJumping = false;
        }
    }
}
