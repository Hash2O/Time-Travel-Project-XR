using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private Vector3 newPortal1;
    [SerializeField] private Vector3 newPortal2;

    [SerializeField] private List<AudioClip> _bruitages;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        newPortal1 = GameObject.Find("Portal 1").transform.position;
        newPortal2 = GameObject.Find("Portal 2").transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
 
    void OnTriggerEnter(Collider other)
    {
        /*
        Vector3 otherPosition = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        Debug.Log(otherPosition);
        */

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Grabbable"))
        {
            _audioSource.clip = _bruitages[0];
            _audioSource.Play();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Grabbable"))
        {
            other.transform.position = newPortal2;
        }
    }


}
