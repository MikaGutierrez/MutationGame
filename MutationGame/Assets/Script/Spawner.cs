using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Self;
    public GameObject[] Everytnig;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateIt());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ActivateIt()
    {
        Instantiate(Everytnig[Random.Range(0, Everytnig.Length)], new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
        yield return new WaitForSeconds(0.0001f);
        Destroy(Self);
    }
}
