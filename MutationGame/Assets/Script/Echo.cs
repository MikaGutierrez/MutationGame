using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    public Animator animationNew;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(ActivateIt());
        }
    }
    private IEnumerator ActivateIt()
    {
        animationNew.Play("EchoWorck");
        yield return new WaitForSeconds(0.6f);
        animationNew.Play("Echo");
    }
}
