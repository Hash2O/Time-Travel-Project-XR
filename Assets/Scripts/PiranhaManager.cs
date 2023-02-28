using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaManager : MonoBehaviour
{
    [SerializeField] Transform _center;
    [SerializeField] float revolutionSpeed;
    private void Start()
    {
        revolutionSpeed = Random.Range(20, 50f);
    }

    private void Update()
    {
        transform.RotateAround(_center.transform.position, Vector3.up, revolutionSpeed * Time.deltaTime);
    }

}
