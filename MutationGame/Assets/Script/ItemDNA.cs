using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDNA : MonoBehaviour
{
    public GameObject Self;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PartPlayer")
        {
            anim.Play("BubblePop");           
        }
    }    
    void DestroyItself()
    {
        Destroy(Self);
    }
}
