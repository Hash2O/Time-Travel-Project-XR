using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterManager : MonoBehaviour
{
    [SerializeField] GameObject firingPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private int _ammo;
    private int _maxBullet;

    public float range = 10000f;
    public LayerMask targetMask;
    public GameObject hitMarkerPrefab; // Préfabriqué de la marque
    public GameObject laserBlast;

    // Start is called before the first frame update
    void Start()
    {
        _maxBullet = _ammo;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnShoot()
    {
        if (_ammo > 0)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            LaserFiring();
            _ammo--;
            
        }

        if (_ammo == 0)
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }

    }
    

    public void LaserFiring()
    {

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(firingPoint.transform.position, firingPoint.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(firingPoint.transform.position, firingPoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            print("Distance : " + hit.distance);
            print("Objet touché : " + hit.collider.name);

            //Put a mark where the laser hits
            GameObject hitMarker = Instantiate(hitMarkerPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            //Mark's lifetime
            Destroy(hitMarker, 5f);

            //Instancier un tir de blaster
            Instantiate(laserBlast, firingPoint.transform.position, firingPoint.transform.rotation);

            //Pour ajouter de l'impact au tir de laser
            //Rigidbody targetRb = hit.rigidbody;
            //targetRb.AddRelativeForce(transform.forward * - 1000f, ForceMode.Impulse);

        }
        else
        {
            Debug.DrawRay(firingPoint.transform.position, firingPoint.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    public void OnReload()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        _ammo = _maxBullet;
    }

}
