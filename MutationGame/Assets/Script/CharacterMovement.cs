using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class CharacterMovement : Audio
{
    [Header("For Sounds")]
    public bool TuchedFloor = true;
    [Header("Death Floats")]
    public float TimerG3;
    private float TimerSecondsG3;
    private float TimerHoursG3;
    private string TimerTextG3;
    public int GenesCountG3 = 0;
    public int RoomsCountG3 = 0;
    public Text TextTimer;
    public Text TextRooms;
    public Text TextGenes;
    public static bool FoundedG2 = false;

    [Header("Movement")]
    private bool WorkWalk = true;
    public float speed;
    private float moveInput;
    public bool IsDead = false;

    [Header("jump Parametrs")]
    public float WeightMM = 1f;
    public bool SecondJump;
    public bool isJumping;
    public float jumpForce = 8;
    public float jumpTimeCounter;
    public float jumpTime;

    [Header("Ground Check")]
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    
    [Header("Rigidbody2D")]
    private Rigidbody2D rb;
    public CapsuleCollider2D cl;
    public SpriteRenderer sp;
    private float RigidbodyOriginalGravityScale;

    [Header("Health")]
    public Image RedGene;
    public Image PurpleGene;
    public Image YellowGene;
    public Image BlueGene;
    public Image GreneHealth;
    public float HPTimer = 1;



    [Header("Animator")]
    public Animator AnimatorForG3;


    [Header("Parts Of The Body")]
    public float jumpForceWings;
    public int WingNumber = 0;
    private int WingNumberOld = 0;
    public float WingTimer = 0;
    public GameObject[] Wings; //None, Bat, Insect, Bird

    public float jumpForceTail;
    public int TailNumber = 0;
    private int TailNumberOld = 0;
    public float TailTimer = 0;
    public GameObject[] Tails; //None, Big, Ball, Rat

    public float jumpForceLeg;
    public float SpeedLeg = 1;
    public int LegNumber = 0;
    private int LegNumberOld = 0;
    public float LegTimer = 0;
    public GameObject[] Legs; //None, 4, 3, 2, funny

    public int SpetialAbilitiesNumber = 0;
    private int SpetialAbilitiesNumberOld = 0;
    public float SpetialAbilitiesTimer = 0;
    public VolumeProfile[] SpetialAbilities; //Normal, RGB, Vingete,Black & White, Oven
    public Volume VolumeG3;

    public GameObject RedParticle;
    public GameObject PurpleParticle;
    public GameObject YellowParticle;
    public GameObject BlueParticle;
    public GameObject BlueTrail;

    
    [Header("Deadth")]
    public GameObject DeadPanel;
    public GameObject TailRenderer;
    private bool TaketDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Girojio2")
        { 
            FoundedG2 = true;
        }
        if (collision.tag == "GravityCollition")
        {
            rb.gravityScale = -0.1f;
        }
        if (collision.tag == "WingDNA")
        {
            PlaySounds(audioClips[1], p1: 0.8f, p2: 1.2f);
            GenesCountG3 += 1;
            StartCoroutine(ActivateWings());
        }
        if (collision.tag == "LegsDNA")
        {
            PlaySounds(audioClips[1], p1: 0.8f, p2: 1.2f);
            GenesCountG3 += 1;
            StartCoroutine(ActivateLegs());
        }
        if (collision.tag == "TailDNA")
        {
            PlaySounds(audioClips[1], p1: 0.8f, p2: 1.2f);
            GenesCountG3 += 1;
            StartCoroutine(ActivateTail());
        }
        if (collision.tag == "SpetialAbilitiesDNA")
        {
            PlaySounds(audioClips[1], p1: 0.8f, p2: 1.2f);
            GenesCountG3 += 1;
            StartCoroutine(ActivateSpetialAbilities());
        }
        if (collision.tag == "CheckGravityCollider")
        { 
            RoomsCountG3 += 1;
        }
        if (collision.tag == "DamageCollider" && TaketDamage == false && IsDead == false)
        {
            PlaySounds(audioClips[2],volume:0.6f, p1: 0.8f, p2: 1.2f);
            rb.velocity = Vector2.up * 10f;
            TakeDamage();
        }
        if (collision.gameObject.layer == 6)
        {
            PlaySounds(audioClips[0],p1:0.8f,p2:1.2f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DamageCollider")
        {
            TaketDamage = false;
        }
        if (collision.tag == "GravityCollition")
        {
            rb.gravityScale = RigidbodyOriginalGravityScale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BlueTrail.SetActive(false);
        TimerG3 = 0;
        DeadPanel.SetActive(false);
        RedGene.enabled =false;
        PurpleGene.enabled = false;
        YellowGene.enabled = false;
        BlueGene.enabled = false;
        foreach (GameObject objet in Wings)
        {
            objet.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
        RigidbodyOriginalGravityScale = rb.gravityScale;
        StartCoroutine(Appear());
    }
    void FixedUpdate()
    {
        if (IsDead == false)
        {
            moveInput = Input.GetAxis("Horizontal");
            if (WorkWalk == true)
            {
                rb.velocity = new Vector2(moveInput * speed * SpeedLeg, rb.velocity.y);
            }
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

        if (isGrounded == true && TuchedFloor == true && IsDead == false)
        {
            TuchedFloor = false;
            PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        }
        if (isGrounded == false)
        {
            TuchedFloor = true;
        }
        //------------- ������
        if (TimerHoursG3 < 10 && TimerSecondsG3 < 10)
        {
            TimerTextG3 = "0" + Mathf.Floor(Mathf.Floor(TimerG3) / 60) + ":0" + (Mathf.Floor(TimerG3) % 60);
        }
        else if (TimerHoursG3 < 10)
        {
            TimerTextG3 = "0" + Mathf.Floor(Mathf.Floor(TimerG3) / 60) + ":" + (Mathf.Floor(TimerG3) % 60);
        }
        else if (TimerSecondsG3 < 10)
        {
            TimerTextG3 = Mathf.Floor(Mathf.Floor(TimerG3) / 60) + ":0" + (Mathf.Floor(TimerG3) % 60);
        }
        else
        {
            TimerTextG3 = Mathf.Floor(Mathf.Floor(TimerG3) / 60) + ":" + (Mathf.Floor(TimerG3) % 60);
        }
        if (IsDead == false)
        {
            TimerG3 += Time.deltaTime;
            TimerHoursG3 = Mathf.Floor(Mathf.Floor(TimerG3) / 60);
            TimerSecondsG3 = (Mathf.Floor(TimerG3) % 60);
        }
        if (HPTimer <= 0)
        {
            StartCoroutine(Die());
        }
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
            SpetialAbilitiesTimer -= Time.deltaTime * 0.05f;
        }
        else
        {
            SpetialAbilitiesNumber = 0;
            VolumeG3.profile = SpetialAbilities[SpetialAbilitiesNumber];
        }


        if (WingNumber + LegNumber + TailNumber + SpetialAbilitiesNumber == 0)
        {
            HPTimer -= Time.deltaTime * 0.05f;
        }
        //--------------------------------���

        if (WingNumber == 1 || WingNumber == 3)
        {
            jumpForceWings = 3 * 2 * WeightMM;
        }
        else if (WingNumber == 2)
        {
            jumpForceWings = 2 * 2 * WeightMM;
        }
        else
        {
            jumpForceWings = 0;
        }
        if (LegNumber == 2)//4 leg
        {
            SpeedLeg = 0.7f;
            jumpForceLeg = 4 * 2 * WeightMM * 0.9f;
        }
        else if (LegNumber == 1)//3 leg
        {
            jumpForceLeg = 4 * 3 * WeightMM;
        }
        else if (LegNumber == 3)//2 leg
        {
            SpeedLeg = 3;
            jumpForceLeg = 2 * 2 * WeightMM * 0.7f;
        }
        else
        {
            SpeedLeg = 1;
            jumpForceLeg = 0;
        }
        if (TailNumber == 1 || TailNumber == 2)// Big Tail & Ball Tail
        {
            jumpForceTail = 3 * WeightMM * 0.7f;
        }
        else if (TailNumber == 3)//Rat 
        {
            jumpForceTail = 4 * WeightMM;
        }
        else
        {
            jumpForceTail = 0;
        }



        //--------------------------------
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (IsDead == false)
        {
            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {

                PlaySounds(audioClips[5], p1: 0.8f, p2: 1.2f);
                isJumping = true;
                SecondJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }
            if (isJumping == false && SecondJump == true && Input.GetKeyDown(KeyCode.Space))
            {

                if (WingNumber != 0)
                {
                    BlueTrail.SetActive(true);
                }
                if (WingNumber == 3)
                {
                    PlaySounds(audioClips[6], p1: 0.8f, p2: 1.2f);
                    jumpTimeCounter = jumpTime;
                    rb.velocity = Vector2.up * jumpForce;
                }
                if (WingNumber == 2)
                {
                    PlaySounds(audioClips[7],p1:0.8f,p2:1.2f);
                    jumpTimeCounter = jumpTime * 2f;
                    rb.velocity = Vector2.up * -0.15f;
                }
                if (WingNumber == 1)
                {
                    PlaySounds(audioClips[6], p1: 0.8f, p2: 1.2f);
                    WorkWalk = false;
                    jumpTimeCounter = 0.17f;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //Char_Animator.SetTrigger("Jump");
                if (jumpTimeCounter > 0 && isJumping == true)
                {
                    rb.velocity = Vector2.up * (jumpForce + jumpForceLeg + jumpForceTail + jumpForceWings);
                    jumpTimeCounter -= Time.deltaTime;
                }
                if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 3 && isJumping == false)
                {
                    BlueTrail.SetActive(true);
                    rb.velocity = Vector2.up * (jumpForce + jumpForceLeg + jumpForceTail + jumpForceWings);
                    jumpTimeCounter -= Time.deltaTime;
                }
                if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 2 && isJumping == false)
                {
                    BlueTrail.SetActive(true);
                    rb.velocity = Vector2.up * -0.2f;
                    jumpTimeCounter -= Time.deltaTime;
                }
                if (jumpTimeCounter > 0 && SecondJump == true && WingNumber == 1 && isJumping == false)
                {
                    BlueTrail.SetActive(true);
                    rb.velocity = Vector2.left * 10f;
                    jumpTimeCounter -= Time.deltaTime;
                }

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (WingNumber == 0 || WingNumber == 2)
                { audioSr.Stop(); }
                BlueTrail.SetActive(false);
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
        SpetialAbilitiesNumber = Random.Range(1, SpetialAbilities.Length);
        yield return new WaitForSeconds(0.0001f);

        if (SpetialAbilitiesNumber == SpetialAbilitiesNumberOld) { StartCoroutine(ActivateSpetialAbilities()); }
        else { VolumeG3.profile = SpetialAbilities[SpetialAbilitiesNumber]; }
    }

    public void TakeDamage()
    {
        AnimatorForG3.Play("G3DamageTaken");
        TaketDamage = true;
        if (WingNumber > 0)
        {
            Instantiate(BlueParticle, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0.3f, 0f));
            WingNumber = 0;
            foreach (GameObject objet in Wings) { objet.SetActive(false); }
        }
        else if (SpetialAbilitiesNumber > 0)
        {
            Instantiate(PurpleParticle, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
            SpetialAbilitiesNumber = 0;
            VolumeG3.profile = SpetialAbilities[SpetialAbilitiesNumber];
        }
        else if (TailNumber > 0)
        {
            Instantiate(YellowParticle, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(-0.5f, 0f, 0f));
            TailNumber = 0;
            foreach (GameObject objet in Tails) { objet.SetActive(false); }
        }
        else if (LegNumber > 0)
        {
            Instantiate(RedParticle, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, -0.3f, 0f));
            LegNumber = 0;
            foreach (GameObject objet in Legs) { objet.SetActive(false); }
        }
        else
        {
            HPTimer -= 0.15f;
        }

    }
    private IEnumerator Die()
    {
        IsDead = true;
        TailRenderer.SetActive(false);
        AnimatorForG3.Play("G3Dies");
        rb.freezeRotation = true;
        rb.velocity = Vector2.up * Vector2.right * 0;
        WingNumber = 0;
        SpetialAbilitiesNumber = 0;
        TailNumber = 0;
        LegNumber = 0;
        foreach (GameObject objet in Wings) { objet.SetActive(false); }
        VolumeG3.profile = SpetialAbilities[SpetialAbilitiesNumber];
        foreach (GameObject objet in Tails) { objet.SetActive(false); }
        foreach (GameObject objet in Legs) { objet.SetActive(false); }
        yield return new WaitForSeconds(0.21f);
        yield return new WaitForSeconds(0.000001f);
        yield return new WaitForSeconds(0.79f);
        sp.enabled = false;
        DeadPanel.SetActive(true);
        TextTimer.text = TimerTextG3;
        TextGenes.text = GenesCountG3 + "";
        TextRooms.text = RoomsCountG3 + "";
    }
    private IEnumerator Appear()
    {
        rb.gravityScale = 0f;
        cl.enabled = false;
        IsDead = true;
        rb.freezeRotation = true;
        AnimatorForG3.Play("G3Appear");
        rb.velocity = Vector2.up * Vector2.right * 0;
        yield return new WaitForSeconds(1.2f);
        AnimatorForG3.Play("G3Idle");
        cl.enabled = true;
        rb.freezeRotation = false;
        IsDead = false;
        rb.gravityScale = RigidbodyOriginalGravityScale;

    }
}
