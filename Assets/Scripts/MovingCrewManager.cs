using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingCrewManager : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] List<GameObject> waypoints = new List<GameObject>();

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
    }

    IEnumerator NextWaypoint()
    {
        index = Random.Range(0, waypoints.Count);
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        agent.SetDestination(waypoints[index].transform.position);
    }
    
}
