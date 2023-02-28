using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDroneManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _trainingDistance;
    [SerializeField] float _speed;
    [SerializeField] float revolutionSpeed;
    [SerializeField] GameObject _trainingProjectile;

    [SerializeField] AudioSource audioSource;

    private float chrono;
    private float maxChrono = 5.0f;
    private bool disabledChrono = false;

    [SerializeField] private bool isCloseEnough;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _trainingDistance = 2f;
        _speed = 1f;
        revolutionSpeed = Random.Range(10, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        chrono += Time.deltaTime;
        print(chrono);
        if (chrono >= maxChrono && !disabledChrono && isCloseEnough)
        {
            fireMode();
            chrono = 0;
        }
    }

    public void closingDistance()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > _trainingDistance)
        {
            print("Closing distance with target");
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) < _trainingDistance)
        {
            print("Keeping distance with target");
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, - _speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, _player.transform.position) <= _trainingDistance)
        {
            isCloseEnough = true;
        }
    }

    public void trainingMode()
    {
        if (isCloseEnough)
        {
            print("Starting Training Mode");
            Transform axis = _player.GetComponentInChildren<Transform>();
            transform.LookAt(_player.transform.position);
            transform.RotateAround(_player.transform.position, Vector3.up, revolutionSpeed * Time.deltaTime);
            if (transform.position.y < 2f)
            {
                transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            }
        }
    }

    public void fireMode()
    {
        print("Fire at the target");

        audioSource.Play();

        Rigidbody _trainingProjectileRb = _trainingProjectile.GetComponent<Rigidbody>();

        Vector3 direction = transform.position - _player.transform.position;

        Instantiate(_trainingProjectile, transform.position, transform.rotation);

        _trainingProjectileRb.AddForce(direction * 50000f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "MetaSaber")
        {
            Debug.Log("Drone hit");
        }
        
    }

}
