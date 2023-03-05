using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownManager : MonoBehaviour
{
    private Vector3 upAndDown;

    [SerializeField] private float speed;

    [SerializeField] float lowerLevel;
    [SerializeField] float higherLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpAndDown();
    }

    void UpAndDown()
    {
        transform.Translate(upAndDown * Time.deltaTime);

        if (transform.localPosition.y <= lowerLevel)//transform.position.y - _flottaison)
        {
            upAndDown = new Vector3(0, speed, 0);
        }
        else if (transform.localPosition.y > higherLevel) //transform.position.y + _flottaison)
        {
            upAndDown = new Vector3(0, - speed, 0);
        }
    }
}
