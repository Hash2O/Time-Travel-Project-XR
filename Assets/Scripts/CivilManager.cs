using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilManager : MonoBehaviour
{

    [SerializeField] private Transform _destination;

    //[SerializeField] private GameObject _civilianRagdoll;

    [SerializeField] private List<AudioClip> _pochette;

    private Transform _player;

    private Transform _enemy;

    private NavMeshAgent _agent;

    private Animator _animator;

    private AudioSource _audioSource;

    [SerializeField] private GameObject[] _enemies;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player").transform;
        // = GameObject.FindWithTag("Enemy").transform;
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemy = _enemies[0].transform;
    }

    // Update is called once per frame
    void Update()
    {

        float dist; //= Vector3.Distance(transform.position, _enemy.position);

        float distToEnemies;

        float closestEnemie = 50.0f;

        foreach (GameObject enemie in _enemies)
        {
            if (enemie)
            {
                distToEnemies = Vector3.Distance(transform.position, enemie.transform.position);

                if (distToEnemies < closestEnemie)
                {
                    closestEnemie = distToEnemies;
                    _enemy.position = enemie.transform.position;
                }
            }
            else
            {
                Debug.Log("I must go to the Spaceship !");


            }


        }

        dist = closestEnemie;


        if (dist < 5.0f) //si trop pres
        {
            escapeFromEnemy();
            ChangeAudioClip(2);
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
        }
        else if (dist < 20.0f)
        {
            escapeFromEnemy();
            ChangeAudioClip(1);
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
        }
        else
        {
            ChangeAudioClip(0);
            Stopfleeing();
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isWalking", false);
        }

        GererLAudio();

    }

    private void ChangeAudioClip(int index)
    {
        if (!_audioSource.isPlaying || _audioSource.clip == _pochette[0])
            _audioSource.clip = _pochette[index];
    }

    private void Stopfleeing()
    {
        _agent.isStopped = true;
    }

    private void escapeFromEnemy()
    {
        _destination.position = transform.position + (transform.position - _enemy.position); // a l'opposé de l'ennemi trouvé 
        _agent.isStopped = false;
        _agent.SetDestination(_destination.position);
    }

    private void GererLAudio()
    {
        int rand;
        rand = Random.Range(1, 4);
        if (rand == 2 && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}
