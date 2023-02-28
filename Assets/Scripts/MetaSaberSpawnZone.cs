using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaSaberSpawnZone : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;

    [SerializeField] Vector3 spawnZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Objet à instancier
            GameObject instantiated = Instantiate(objectPrefab);

            //Spawn aléatoire dans la zone définie au préalable
            instantiated.transform.position = new Vector3(
                Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2),
                Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2),
                Random.Range(transform.position.z - spawnZone.z / 2, transform.position.z + spawnZone.z / 2)
                );
        }
        */
    }

    public void SpawningTrainingDrones()
    {
        //Objet à instancier
        GameObject instantiated = Instantiate(objectPrefab);

        //Spawn aléatoire dans la zone définie au préalable
        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2),
            Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2),
            Random.Range(transform.position.z - spawnZone.z / 2, transform.position.z + spawnZone.z / 2)
            );
    }
}
