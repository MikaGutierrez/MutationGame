using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawningG2 : MonoBehaviour
{
    public GameObject[] NextRooms;
    private bool AlreadyEnteret = false;
    public static int MinOne;
    public int RandomInt = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && AlreadyEnteret == false)
        {
            RandomInt = Random.Range(0, NextRooms.Length - MinOne);
            AlreadyEnteret = true;
            Instantiate(NextRooms[Random.Range(0, NextRooms.Length)], new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RandomInt = Random.Range(0, NextRooms.Length - MinOne);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MinOne);
        if (RandomInt == 4)
        {
            Debug.Log("dfdf");
            MinOne = 1;
        }

    }
}
