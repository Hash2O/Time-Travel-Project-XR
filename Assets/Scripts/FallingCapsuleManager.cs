using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCapsuleManager : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            print("It hits the ground");
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
