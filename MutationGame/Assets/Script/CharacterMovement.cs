using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //For Movement
    private bool WorkWalk = true;
    public float speed;
    private float moveInput;

    //For Jump
    public bool SecondJump;
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

    //PartsOfTheBody
    public int WingNumber = 0;
    private int WingNumberOld = 0;
    public GameObject[] Wings; //None, Bat, Insect, Bird

    public int TailNumber = 0;
    private int TailNumberOld = 0;
    public GameObject[] Tails; //None, Big, Ball, Rat

    public int LegNumber = 0;
    private int LegNumberOld = 0;
    public GameObject[] Legs; //None, 4, 3, 2, funny

    public int SpetialAbilitiesNumber = 0;
    private int SpetialAbilitiesNumberOld = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WingDNA")
        {
            StartCoroutine(ActivateWings());
        }
        if (collision.tag == "LegsDNA")
        {
            StartCoroutine(ActivateLegs());
        }
        if (collision.tag == "TailDNA")
        {
            StartCoroutine(ActivateTail());
            WingNumber = Random.Range(1, 4);
        }
        if (collision.tag == "SpetialAbilitiesDNA")
        {
            StartCoroutine(ActivateSpetialAbilities());
            WingNumber = Random.Range(1, 5);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject objet in Wings)
        {
            objet.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (WorkWalk == true)
        {
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
            SecondJump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (isJumping == false && SecondJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (WingNumber == 3)
            {
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }
            if (WingNumber == 2)
            {
                jumpTimeCounter = jumpTime * 2f;
                rb.velocity = Vector2.up * -0.3f;
            }
            if (WingNumber == 1)
            {
                WorkWalk = false;
                jumpTimeCounter = 0.17f;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //Char_Animator.SetTrigger("Jump");
            if (jumpTimeCounter > 0 && isJumping == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 3 && isJumping == false)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 2 && isJumping == false)
            {
                rb.velocity = Vector2.up * -0.3f;
                jumpTimeCounter -= Time.deltaTime;
            }
            if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 1 && isJumping == false)
            {
                rb.velocity = Vector2.left * 10f;
                jumpTimeCounter -= Time.deltaTime;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space) )
        {
            if (isJumping == true && SecondJump == true)
            {
                isJumping = false;
            }
            else if (isJumping == false && SecondJump == true)
            {
                SecondJump = false;
            }
        }

        if (WorkWalk == false && WingNumber == 1 && jumpTimeCounter > 0)
        {
            if (moveInput < 0)
            { 
                rb.velocity = Vector2.left * 50f;
                jumpTimeCounter -= Time.deltaTime;
            }
            if (moveInput > 0)
            {
                rb.velocity = Vector2.right * 50f;
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        else if (WorkWalk == false && jumpTimeCounter <= 0)
        {
            WorkWalk = true;
        }


        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(ActivateWings());
        }
    }
    private IEnumerator ActivateWings()
    {
        WingNumberOld = WingNumber;
        yield return new WaitForSeconds(0.0001f);
        WingNumber = Random.Range(1, 4);
        yield return new WaitForSeconds(0.0001f);

        if (WingNumber == WingNumberOld){StartCoroutine(ActivateWings());}
        else{foreach (GameObject objet in Wings){objet.SetActive(false);}}

        Wings[WingNumber].SetActive(true);
    }
    private IEnumerator ActivateLegs()
    {
        LegNumberOld = LegNumber;
        yield return new WaitForSeconds(0.0001f);
        LegNumber = Random.Range(1, 5);
        yield return new WaitForSeconds(0.0001f);

        if (LegNumber == LegNumberOld) {StartCoroutine(ActivateLegs()); }
        else { foreach (GameObject objet in Legs) { objet.SetActive(false); } }

        Legs[LegNumber].SetActive(true);
    }
    private IEnumerator ActivateTail()
    {
        TailNumberOld = TailNumber;
        yield return new WaitForSeconds(0.0001f);
        TailNumber = Random.Range(1, 5);
        yield return new WaitForSeconds(0.0001f);

        if (TailNumber == TailNumberOld) { StartCoroutine(ActivateTail()); }
        else { foreach (GameObject objet in Tails) { objet.SetActive(false); } }

        Tails[TailNumber].SetActive(true);
    }
    private IEnumerator ActivateSpetialAbilities()
    {
        SpetialAbilitiesNumberOld = SpetialAbilitiesNumber;
        yield return new WaitForSeconds(0.0001f);
        SpetialAbilitiesNumber = Random.Range(1, 5);
        yield return new WaitForSeconds(0.0001f);

        if (SpetialAbilitiesNumber == SpetialAbilitiesNumberOld) { StartCoroutine(ActivateSpetialAbilities()); }
        //else { foreach (GameObject objet in SpetialAbilitie) { objet.SetActive(false); } }
        //Tails[TailNumber].SetActive(true);
    }
}
