using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallenBoxesCounter : MonoBehaviour
{
    public int counter;
    [SerializeField] TextMeshProUGUI boxCounter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        boxCounter.SetText("Score : " + counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target")
        {
            counter++;
            print(counter);
            boxCounter.SetText("Score : " + counter);
        }
    }
}
