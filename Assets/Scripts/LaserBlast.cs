using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlast : MonoBehaviour
{
    public float speed = 50f;
    public float distance = 100f;

    void Start()
    {
        Destroy(gameObject, distance / speed);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
