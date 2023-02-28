using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionPrefab;
    [SerializeField] private UnityEvent collisionEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log(collision.collider.name + " est entré en collision avec " + gameObject.name);
            //collisionEvent.Invoke(); //On appelle l'évènement créé plus haut. Gestion via Unity
        if(collision.collider.CompareTag("Enemy"))
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Debug.Log("Touché !");
        }
    }
}
