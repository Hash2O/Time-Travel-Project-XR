using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingYBotManager : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator ybotAnim;

    [SerializeField] List<GameObject> waypoints = new List<GameObject>();

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ybotAnim = GetComponent<Animator>();

        agent.SetDestination(waypoints[0].transform.position);

        ybotAnim.SetBool("isMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            ybotAnim.SetBool("isMoving", false);
            StartCoroutine("NextWaypoint");
        }
    }

    IEnumerator NextWaypoint()
    {
        index = Random.Range(0, waypoints.Count);
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        ybotAnim.SetBool("isMoving", true);
        agent.SetDestination(waypoints[index].transform.position);
    }
}
