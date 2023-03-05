using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XWingManager : MonoBehaviour
{
    private float speed;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y > 0)
        {
            //audioSource.Play();
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else if (transform.localPosition.y >= 0)
        {
            print("landing");
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
