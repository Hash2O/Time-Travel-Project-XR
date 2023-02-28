using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetManager : MonoBehaviour
{
    public float _xSpeed = 1.0f;
    private Vector3 direction;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;
    private int hitCounter;
    [SerializeField] TextMeshProUGUI hitCounterText;
    // Start is called before the first frame update
    void Start()
    {
        hitCounter = 0;
        hitCounterText.SetText("Hits : " + hitCounter);
    }

    // Update is called once per frame
    void Update()
    {
        //Déplacement
        transform.Translate(direction * Time.deltaTime);

        //Conditions
        if (transform.position.x <= -1)
        {
            //Debug.Log("Depart");
            direction = new Vector3(- _xSpeed, 0, 0);
        }
        else if (transform.position.x > 4.5)
        {
            //Debug.Log("Au dela de la limite");
            direction = new Vector3( _xSpeed, 0, 0);
        }

        print(gameObject.name + " a reçu " + hitCounter + " tirs de votre part.");

        if (hitCounter == 5)
        {
            if(audioSource)
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            Destroy(gameObject, 1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            hitCounter++;
            hitCounterText.SetText("Hits : " + hitCounter);
            collision.gameObject.SetActive(false);
        }

    }
}
