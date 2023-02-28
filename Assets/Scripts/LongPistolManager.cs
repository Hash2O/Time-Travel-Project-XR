using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LongPistolManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootAction;

    [SerializeField] private InputActionReference _reloadAction;

    private bool _isEquiped;    //False par défaut

    [SerializeField] private Transform _gunHoldPoint;

    [SerializeField] private GameObject _prefabBullet;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _force;

    [SerializeField] private int _ammo;

    [SerializeField] private ParticleSystem _gunPowder;

    private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClips;

    private int _maxBullet;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation de l'évènement et de l'action associée
        //Equivalent d'un AddListener
        _shootAction.action.Enable();
        _shootAction.action.performed += FireWithShotgun; //Evènement auque on associe une fonction

        _reloadAction.action.Enable();
        _reloadAction.action.performed += ReloadShotgun;

        _maxBullet = _ammo;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReloadShotgun(InputAction.CallbackContext obj)
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        _ammo = _maxBullet;
        Debug.Log("Ammo : " + _ammo);
    }

    private void FireWithShotgun(InputAction.CallbackContext obj) //param nécessaire pour appeler la fonction avec l'évènement
    {

        if (_isEquiped && _ammo > 0)  //Test 
        {

            //Implémentation de la logique OnThrow (attachée au FPC)
            //NB : ne pas oublier de mapper l'action dans le StarterAssets-Inputs (créer une nouvelle map si besoin pour clarifier)
            Debug.Log("Fire");

            audioSource.clip = audioClips[1];
            audioSource.Play();

            GameObject bullet = Instantiate(_prefabBullet, _spawnPoint.position, Quaternion.identity);

            _gunPowder.Play();

            _ammo--;

            Debug.Log("Ammo : " + _ammo);

            bullet.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * _force, ForceMode.Impulse);

            Destroy(bullet, 2.0f);

        }

        if (_isEquiped && _ammo == 0)
        {
            audioSource.clip = audioClips[3];
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /**********************************************
             * 
             * Mecanique de base
             * 
             *********************************************/
            _isEquiped = true;
            //Position et rotation
            transform.position = _gunHoldPoint.position;
            transform.rotation = _gunHoldPoint.rotation;
            //Attacher l'objet au player
            transform.parent = _gunHoldPoint;

            /*******************************
            transform.parent = other.transform.GetChild(0);
            transform.position = other.transform.GetChild(1).position;
            transform.rotation = other.transform.GetChild(0).rotation;

            *******************************/

            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }
}
