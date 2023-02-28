using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] Rigidbody projectileRb;
    [SerializeField] AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        projectileRb.AddRelativeForce(Vector3.forward * 500f);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("That's a hit !");
            //Destroy(gameObject);
            /*
            //Pour faire "exploser" le zombie
            Destroy(collision.gameObject);
            Instantiate(_explosionParticle, transform.position, transform.rotation);
            */
            gameObject.SetActive(false);
        }
    }
}
