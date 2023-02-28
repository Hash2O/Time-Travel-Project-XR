using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFiringArrow()
    {
        var arrow = Instantiate(arrowPrefab, transform.parent.position, transform.parent.rotation);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(Vector3.forward * 20f, ForceMode.Impulse);
    }
}
