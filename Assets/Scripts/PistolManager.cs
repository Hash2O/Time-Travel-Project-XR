using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolManager : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _projectileSpawnPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private int _ammo;
    private int _maxBullet;
    // Start is called before the first frame update
    void Start()
    {
        //_maxBullet = _ammo;
        _maxBullet = 20;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot()
    {
        if(_ammo >0)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            GameObject bullet = Instantiate(_projectilePrefab, _projectileSpawnPoint.transform.position, _projectileSpawnPoint.transform.rotation);
            _ammo--;
            Destroy(bullet, 2.0f);
        }

        if(_ammo == 0)
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }

    }

    public void OnReload()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        _ammo = _maxBullet;
    }
}
