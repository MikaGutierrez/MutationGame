using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDNA : MonoBehaviour
{
    public GameObject Self;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(Self);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
