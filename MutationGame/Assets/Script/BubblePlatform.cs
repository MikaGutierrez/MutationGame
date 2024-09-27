using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator AnimatorPlatform;
    public GameObject Self;
    public GameObject SpawnPoint;
    public bool PlayerOn = false;

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
        if (PlayerOn == true)
        {
            rb.velocity = Vector2.up * -1.2f;
        }
        else
        {
            rb.velocity = Vector2.up * 0;
        }
     }

        void Respawn()
    {
        Self.transform.position = SpawnPoint.transform.position;
        AnimatorPlatform.Play("BubblePlatformIdle");
        PlayerOn = false;
    }
}
