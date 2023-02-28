using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienManagement : MonoBehaviour
{
    private NavMeshAgent _agent;

    //[SerializeField] private Transform _destination;

    [SerializeField] private Transform _player;

    [SerializeField] private Transform _civil;

    [SerializeField] private ParticleSystem _explosionParticle;

    [SerializeField] private List<AudioClip> _pochette;

    private Animator _animatorAnim;

    private AudioSource _audioSource;

    [SerializeField] private GameObject _damageImage;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _player = GameObject.FindWithTag("Player").transform;

        _civil = GameObject.FindWithTag("Civil").transform;

        _animatorAnim = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        //_agent.SetDestination(_player.position);

        //Gère le décalage dans les animations des aliens
        _animatorAnim.SetFloat("offset", Random.Range(0.0f, 1.0f)); //Génération aléatoire du décalage (offset) pour chaque zombie

        //Gère la playlist des sons associés aux aliens
        _audioSource.clip = _pochette[Random.Range(0, _pochette.Count)];

    }

    // Update is called once per frame
    void Update()
    {
        //Déterminer la distance entre l'alien prefab et le joueur
        float distToPlayer = Vector3.Distance(transform.position, _player.position);
        float distToCivil = Vector3.Distance(transform.position, _civil.position);

        float distToTarget;

        if (distToPlayer < distToCivil)
        {
            distToTarget = distToPlayer;
        }
        else
        {
            distToTarget = distToCivil;
        }

        //Si le joueur est à plus de 25 mêtres, l'alien reste sur place
        if(distToTarget > 50.0f)
        {
            //_agent.isStopped = true;
            _animatorAnim.SetBool("isStopped", true);
        }
        else if (distToTarget > 2.0f)
        {
            //Entre 25 et 2 mêtres, l'alien se dirige vers le joueur
            //_agent.isStopped = false;
            _animatorAnim.SetBool("isStopped", false);
            _agent.SetDestination(_player.position);
            _animatorAnim.SetBool("isAttacking", false);
            if (_damageImage)
            {
                _damageImage.SetActive(false);
            }
        }
        else
        {
            if (_agent.isStopped)
            {
                _animatorAnim.SetBool("isStopped", false);
            }
            //Moins de deux mètres, le zombie enclenche l'anim d'attaque
            _animatorAnim.SetBool("isAttacking", true);
            _damageImage.SetActive(true);
            Debug.Log("<color=#FF0000>GNAAAP ! </color>");
        }

        //Penser au frameRate, environ 60 par secondes
        //Ici, on fait grogner les zombies de façon aléatoire
        if (!_audioSource.isPlaying)
        {
            int aleatoire = Random.Range(1, 100);

            if (aleatoire > 95)
            {
                _audioSource.clip = _pochette[Random.Range(0, _pochette.Count)];
                _audioSource.Play();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Debug.Log("Alien hit !");
            //GetComponent<BoxCollider>().enabled = false;

            //Explosion liée au tir
            Instantiate(_explosionParticle, transform.position, transform.rotation);

            //Alien stoppé dans son déplacement
            _agent.speed = 0;
            _agent.isStopped = true;

            //Booleen pour déclencher l'anim fatale
            _animatorAnim.SetBool("isStopped", true);
            _animatorAnim.SetBool("isDead", true);

            _audioSource.clip = _pochette[3];
            _audioSource.Play();
            
            //Destruction du gameobject
            Destroy(gameObject, 2.0f);
        }


    }
}
