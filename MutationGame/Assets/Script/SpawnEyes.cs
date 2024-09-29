using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnEyes : Audio
{
    public GameObject Eyes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnerEyes()
    {
        Instantiate(Eyes, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f));
    }
    public void PlayIt()
    {
        PlaySounds(audioClips[0]);
    }
    public void PlayIt2()
    {
        PlaySounds(audioClips[0]);
    }
}
