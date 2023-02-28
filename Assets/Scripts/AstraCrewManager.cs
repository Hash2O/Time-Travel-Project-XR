using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstraCrewManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void astraCrewComments()
    {
        audioSource.PlayOneShot(clips[1]);
    }
}
