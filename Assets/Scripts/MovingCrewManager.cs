using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingCrewManager : MonoBehaviour
{
    private NavMeshAgent agent;

    private AudioSource audioSource;

    [SerializeField] List<GameObject> waypoints = new List<GameObject>();

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();

        agent.SetDestination(waypoints[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < 0.5f)
        {
            print("Next waypoint");
            StartCoroutine("NextWaypoint");
        }

        if (!audioSource.isPlaying)
        {
            int aleatoire = Random.Range(1, 101);

            if (aleatoire > 99)
            {
                audioSource.Play();
            }
        }
    }

    IEnumerator NextWaypoint()
    {
        index = Random.Range(0, waypoints.Count);
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        agent.SetDestination(waypoints[index].transform.position);
    }
    
}
