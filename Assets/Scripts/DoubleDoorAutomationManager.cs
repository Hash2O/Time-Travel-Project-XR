using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorAutomationManager : MonoBehaviour
{
    [SerializeField] private Animation doorAnimation;
    [SerializeField] private AudioSource doorAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!doorAnimation.isPlaying)
        {
            print("opening doors");
            doorAnimation.Play();
        }
        if(!doorAudioSource.isPlaying)
        {
            doorAudioSource.Play();
        }
        
    }
}
