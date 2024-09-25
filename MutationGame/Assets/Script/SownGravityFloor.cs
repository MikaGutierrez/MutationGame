using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SownGravityFloor : MonoBehaviour
{
    public GameObject Obj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Obj.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
