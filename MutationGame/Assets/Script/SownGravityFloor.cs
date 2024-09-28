using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SownGravityFloor : MonoBehaviour
{
    public GameObject Obj;
    private BoxCollider2D cl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Obj.SetActive(true);
            cl.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<BoxCollider2D>();
        Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
