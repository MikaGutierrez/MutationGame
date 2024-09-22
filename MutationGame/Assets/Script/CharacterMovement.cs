using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //Health
    public Image RedGene;
    public Image PurpleGene;
    public Image YellowGene;
    public Image BlueGene;
    public Image GreneHealth;
    public float HPTimer = 1;






    //PartsOfTheBody
    public int WingNumber = 0;
    private int WingNumberOld = 0;
    public float WingTimer = 0;
    public GameObject[] Wings; //None, Bat, Insect, Bird

    public int TailNumber = 0;
    private int TailNumberOld = 0;
    public float TailTimer = 0;
    public GameObject[] Tails; //None, Big, Ball, Rat

    public int LegNumber = 0;
    private int LegNumberOld = 0;
    public float LegTimer = 0;
    public GameObject[] Legs; //None, 4, 3, 2, funny

    public int SpetialAbilitiesNumber = 0;
    private int SpetialAbilitiesNumberOld = 0;
    public float SpetialAbilitiesTimer = 0;
    public GameObject[] SpetialAbilities; //None, Dark

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
        }
        if (collision.tag == "SpetialAbilitiesDNA")
        {
            StartCoroutine(ActivateSpetialAbilities());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RedGene.enabled =false;
        PurpleGene.enabled = false;
        YellowGene.enabled = false;
        BlueGene.enabled = false;
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
        }
        //if (moveInput > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        //else if (moveInput < 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        GreneHealth.fillAmount = HPTimer;

        if (WingNumber > 0){BlueGene.enabled = true;}
        else {BlueGene.enabled = false; }
        BlueGene.fillAmount = WingTimer;

        if (TailNumber > 0){YellowGene.enabled = true;}
        else { YellowGene.enabled = false; }
        YellowGene.fillAmount = TailTimer;

        if (LegNumber > 0){RedGene.enabled = true;}
        else {RedGene.enabled = false; }
        RedGene.fillAmount = LegTimer;

        if (SpetialAbilitiesNumber > 0){PurpleGene.enabled = true;}
        else {PurpleGene.enabled = false; }
        PurpleGene.fillAmount = SpetialAbilitiesTimer;


        if (WingTimer >= 0)
        {
            WingTimer -= Time.deltaTime * 0.1f;
        }
        else 
        {
            WingNumber = 0;
            foreach (GameObject objet in Wings) { objet.SetActive(false);}
        }

        if (TailTimer >= 0)
        {
            TailTimer -= Time.deltaTime * 0.1f;
        }
        else
        {
            TailNumber = 0;
            foreach (GameObject objet in Tails) { objet.SetActive(false); }
        }

        if (LegTimer >= 0)
        {
            LegTimer -= Time.deltaTime * 0.1f;
        }
        else
        {
            LegNumber = 0;
            foreach (GameObject objet in Legs) { objet.SetActive(false); }
        }

        if (SpetialAbilitiesTimer >= 0)
        {
            SpetialAbilitiesTimer -= Time.deltaTime * 0.1f;
        }
        else
        {
            SpetialAbilitiesNumber = 0;
            foreach (GameObject objet in SpetialAbilities) { objet.SetActive(false); }
        }


        if (WingNumber + LegNumber + TailNumber + SpetialAbilitiesNumber == 0)
        {
            HPTimer -= Time.deltaTime * 0.05f;
        }
        //--------------------------------

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


        //if (Input.GetKeyUp(KeyCode.E))
        //{
        //    StartCoroutine(ActivateWings());
        //}
    }
    private IEnumerator ActivateWings()
    {
        WingTimer = 1;
        WingNumberOld = WingNumber;
        yield return new WaitForSeconds(0.0001f);
        BlueGene.enabled = true;
        WingNumber = Random.Range(1, 4);
        yield return new WaitForSeconds(0.0001f);

        if (WingNumber == WingNumberOld){StartCoroutine(ActivateWings());}
        else{foreach (GameObject objet in Wings){objet.SetActive(false);}}

        Wings[WingNumber].SetActive(true);
    }
    private IEnumerator ActivateLegs()
    {
        LegTimer = 1;
        LegNumberOld = LegNumber;
        yield return new WaitForSeconds(0.0001f);
        RedGene.enabled = true;
        LegNumber = Random.Range(1, 4);
        yield return new WaitForSeconds(0.0001f);

        if (LegNumber == LegNumberOld) {StartCoroutine(ActivateLegs()); }
        else { foreach (GameObject objet in Legs) { objet.SetActive(false); } }

        Legs[LegNumber].SetActive(true);
    }
    private IEnumerator ActivateTail()
    {
        TailTimer = 1;
        TailNumberOld = TailNumber;
        yield return new WaitForSeconds(0.0001f);
        TailNumber = Random.Range(1, 4);
        YellowGene.enabled = true;
        yield return new WaitForSeconds(0.0001f);

        if (TailNumber == TailNumberOld) { StartCoroutine(ActivateTail()); }
        else { foreach (GameObject objet in Tails) { objet.SetActive(false); } }

        Tails[TailNumber].SetActive(true);
    }
    private IEnumerator ActivateSpetialAbilities()
    {
        SpetialAbilitiesTimer = 1;
        SpetialAbilitiesNumberOld = SpetialAbilitiesNumber;
        yield return new WaitForSeconds(0.0001f);
        PurpleGene.enabled = true;
        SpetialAbilitiesNumber = Random.Range(1, 2);
        yield return new WaitForSeconds(0.0001f);

        if (SpetialAbilitiesNumber == SpetialAbilitiesNumberOld) { StartCoroutine(ActivateSpetialAbilities()); }
        else { foreach (GameObject objet in SpetialAbilities) { objet.SetActive(false); } }
        SpetialAbilities[SpetialAbilitiesNumber].SetActive(true);
    }
}
