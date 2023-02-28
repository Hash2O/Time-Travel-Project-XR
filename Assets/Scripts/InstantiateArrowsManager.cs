using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateArrowsManager : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
       projectileRb = projectilePrefab.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShoot()
    {
        GameObject arrow = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
    }
}
